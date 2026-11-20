-- Execute uma vez no banco limk existente, depois de fazer um backup.
-- Nao execute limk_banco.sql sobre o banco que ja contem seus dados.
-- Os valores usados pela ficha atual (atleta) tem prioridade no vinculo principal.
-- Vinculos adicionais, seus IDs e os quadros existentes sao preservados.
-- ALTER TABLE confirma alteracoes no MySQL; esta migracao nao e uma transacao unica.

USE limk;

DELIMITER $$

CREATE PROCEDURE limk_migracao_001_normalizar_atletas()
BEGIN
    DECLARE nome_indice VARCHAR(64);
    DECLARE nome_chave VARCHAR(64);
    DECLARE quantidade_indices INT;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        DROP TEMPORARY TABLE IF EXISTS limk_migracao_001_vinculos;
        RESIGNAL;
    END;

    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns
        WHERE table_schema = DATABASE() AND table_name = 'atleta'
          AND column_name = 'idEquipe'
    ) OR EXISTS (
        SELECT 1 FROM information_schema.columns
        WHERE table_schema = DATABASE() AND table_name = 'equipe_atleta'
          AND column_name = 'titular'
    ) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Estrutura diferente da original ou migracao 001 ja executada.';
    END IF;

    SELECT COUNT(*), MAX(index_name) INTO quantidade_indices, nome_indice
    FROM (
        SELECT index_name
        FROM information_schema.statistics
        WHERE table_schema = DATABASE() AND table_name = 'equipe_atleta'
          AND non_unique = 0
        GROUP BY index_name
        HAVING GROUP_CONCAT(column_name ORDER BY seq_in_index) = 'equipe_id,numero_camisa'
    ) AS indices_camisa;

    IF quantidade_indices <> 1 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Confira o indice unico de equipe e numero da camisa antes de migrar.';
    END IF;

    IF EXISTS (
        SELECT 1 FROM atleta AS a
        LEFT JOIN equipe AS e ON e.id = a.idEquipe
        WHERE e.id IS NULL
    ) OR EXISTS (
        SELECT 1 FROM equipe_atleta AS ea
        LEFT JOIN atleta AS a ON a.id = ea.atleta_id
        WHERE a.id IS NULL
    ) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Existem vinculos sem atleta ou equipe correspondente. Corrija antes de migrar.';
    END IF;

    -- Monta e valida o resultado antes de mudar as tabelas permanentes.
    CREATE TEMPORARY TABLE limk_migracao_001_vinculos (
        id INT NULL,
        equipe_id INT NOT NULL,
        atleta_id INT NOT NULL,
        numero_camisa INT NOT NULL,
        titular BOOLEAN NOT NULL,
        ativo BOOLEAN NOT NULL
    );

    INSERT INTO limk_migracao_001_vinculos
        (id, equipe_id, atleta_id, numero_camisa, titular, ativo)
    SELECT ea.id, ea.equipe_id, ea.atleta_id,
           CASE WHEN ea.equipe_id = a.idEquipe THEN a.numero_camisa ELSE ea.numero_camisa END,
           CASE WHEN ea.equipe_id = a.idEquipe THEN a.titular ELSE FALSE END,
           ea.ativo
    FROM equipe_atleta AS ea
    JOIN atleta AS a ON a.id = ea.atleta_id;

    INSERT INTO limk_migracao_001_vinculos
        (id, equipe_id, atleta_id, numero_camisa, titular, ativo)
    SELECT NULL, a.idEquipe, a.id, a.numero_camisa, a.titular, TRUE
    FROM atleta AS a
    LEFT JOIN equipe_atleta AS ea ON ea.atleta_id = a.id AND ea.equipe_id = a.idEquipe
    WHERE ea.id IS NULL;

    IF EXISTS (
        SELECT 1 FROM limk_migracao_001_vinculos
        WHERE numero_camisa NOT BETWEEN 1 AND 99
    ) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Numeros de camisa invalidos. Nenhuma tabela foi alterada.';
    END IF;

    IF EXISTS (
        SELECT 1 FROM limk_migracao_001_vinculos
        GROUP BY equipe_id, numero_camisa HAVING COUNT(*) > 1
    ) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Numeros de camisa invalidos ou repetidos na mesma equipe. Nenhuma tabela foi alterada.';
    END IF;

    ALTER TABLE equipe_atleta
        ADD COLUMN titular BOOLEAN NOT NULL DEFAULT FALSE AFTER numero_camisa;

    -- Retira o indice apenas durante a troca dos numeros, que podem estar permutados.
    SET @limk_migracao_sql = CONCAT(
        'ALTER TABLE equipe_atleta DROP INDEX `', REPLACE(nome_indice, '`', '``'), '`');
    PREPARE limk_migracao_stmt FROM @limk_migracao_sql;
    EXECUTE limk_migracao_stmt;
    DEALLOCATE PREPARE limk_migracao_stmt;

    UPDATE equipe_atleta AS ea
    JOIN limk_migracao_001_vinculos AS v ON v.id = ea.id
    SET ea.numero_camisa = v.numero_camisa, ea.titular = v.titular;

    INSERT INTO equipe_atleta (equipe_id, atleta_id, numero_camisa, titular, ativo)
    SELECT equipe_id, atleta_id, numero_camisa, titular, ativo
    FROM limk_migracao_001_vinculos
    WHERE id IS NULL
    ORDER BY atleta_id;

    SET @limk_migracao_sql = CONCAT(
        'ALTER TABLE equipe_atleta ADD UNIQUE INDEX `', REPLACE(nome_indice, '`', '``'),
        '` (equipe_id, numero_camisa)');
    PREPARE limk_migracao_stmt FROM @limk_migracao_sql;
    EXECUTE limk_migracao_stmt;
    DEALLOCATE PREPARE limk_migracao_stmt;

    -- Os dados da ficha ja foram copiados. Agora remove apenas as colunas duplicadas.
    SELECT MAX(constraint_name) INTO nome_chave
    FROM information_schema.key_column_usage
    WHERE table_schema = DATABASE() AND table_name = 'atleta'
      AND column_name = 'idEquipe' AND referenced_table_name = 'equipe';

    IF nome_chave IS NOT NULL THEN
        SET @limk_migracao_sql = CONCAT(
            'ALTER TABLE atleta DROP FOREIGN KEY `', REPLACE(nome_chave, '`', '``'), '`');
        PREPARE limk_migracao_stmt FROM @limk_migracao_sql;
        EXECUTE limk_migracao_stmt;
        DEALLOCATE PREPARE limk_migracao_stmt;
    END IF;

    ALTER TABLE atleta
        DROP COLUMN numero_camisa,
        DROP COLUMN idEquipe,
        DROP COLUMN titular;

    DROP TEMPORARY TABLE limk_migracao_001_vinculos;
END$$

DELIMITER ;

CALL limk_migracao_001_normalizar_atletas();
DROP PROCEDURE limk_migracao_001_normalizar_atletas;
