-- Execute uma vez, depois da migracao 002 e de fazer um backup do banco.
-- Renomeia as colunas sem modificar as cores ja escolhidas pelos usuarios.

USE limk;

ALTER TABLE preferencia_usuario
    RENAME COLUMN cor_jogador_principal TO cor_atleta_principal,
    RENAME COLUMN cor_jogador_adversario TO cor_atleta_adversario;
