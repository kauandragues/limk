-- LIMK Volleyball | Correção das formações de demonstração em uma base já instalada
-- Execute o arquivo inteiro no MySQL 8.0.46. A base usada pela aplicação é limk.
-- Cada fase é reconhecida pela tática, pelo nome, pelas 12 posições e pela bola da carga antiga.
-- A correção preserva as táticas pessoais e as fases cujas posições já foram editadas.
SET NAMES utf8mb4;
USE limk;

WITH
taticas_demo (id, equipe_id, nome) AS (
    VALUES
        ROW(1, 1, 'Recepção inicial'),
        ROW(2, 1, 'Ataque pela entrada'),
        ROW(3, 2, 'Organização defensiva'),
        ROW(4, 2, 'Cobertura do ataque')
),
fases_demo (tatica_id, modelo, nome, bola_x, bola_y) AS (
    VALUES
        ROW(1, 1, 'Recepção inicial', 0.5, 0.18),
        ROW(1, 2, 'Ajuste dos passadores', 0.38, 0.82),
        ROW(1, 3, 'Preparação para levantamento', 0.68, 0.58),
        ROW(2, 1, 'Posicionamento inicial', 0.68, 0.58),
        ROW(2, 2, 'Aproximação do ataque', 0.22, 0.56),
        ROW(2, 3, 'Finalização pela entrada', 0.3, 0.22),
        ROW(3, 1, 'Organização defensiva', 0.25, 0.2),
        ROW(3, 2, 'Ajuste da defesa', 0.72, 0.83),
        ROW(3, 3, 'Defesa do ataque', 0.62, 0.6),
        ROW(4, 1, 'Posicionamento de cobertura', 0.22, 0.56),
        ROW(4, 2, 'Aproximação para cobertura', 0.22, 0.51),
        ROW(4, 3, 'Cobertura do atacante', 0.35, 0.66)
),
-- Os três padrões de formação originais se repetiam nas quatro táticas.
formacoes_antigas (modelo, lado, x, y) AS (
    VALUES
        ROW(1, 'PRINCIPAL', 0.78, 0.88),
        ROW(1, 'PRINCIPAL', 0.78, 0.62),
        ROW(1, 'PRINCIPAL', 0.22, 0.62),
        ROW(1, 'PRINCIPAL', 0.22, 0.88),
        ROW(1, 'PRINCIPAL', 0.5, 0.62),
        ROW(1, 'PRINCIPAL', 0.5, 0.88),
        ROW(1, 'ADVERSARIO', 0.22, 0.12),
        ROW(1, 'ADVERSARIO', 0.22, 0.38),
        ROW(1, 'ADVERSARIO', 0.78, 0.38),
        ROW(1, 'ADVERSARIO', 0.78, 0.12),
        ROW(1, 'ADVERSARIO', 0.5, 0.38),
        ROW(1, 'ADVERSARIO', 0.5, 0.12),
        ROW(2, 'PRINCIPAL', 0.805, 0.855),
        ROW(2, 'PRINCIPAL', 0.755, 0.595),
        ROW(2, 'PRINCIPAL', 0.245, 0.595),
        ROW(2, 'PRINCIPAL', 0.195, 0.855),
        ROW(2, 'PRINCIPAL', 0.525, 0.595),
        ROW(2, 'PRINCIPAL', 0.475, 0.855),
        ROW(2, 'ADVERSARIO', 0.195, 0.145),
        ROW(2, 'ADVERSARIO', 0.245, 0.405),
        ROW(2, 'ADVERSARIO', 0.755, 0.405),
        ROW(2, 'ADVERSARIO', 0.805, 0.145),
        ROW(2, 'ADVERSARIO', 0.475, 0.405),
        ROW(2, 'ADVERSARIO', 0.525, 0.145),
        ROW(3, 'PRINCIPAL', 0.83, 0.83),
        ROW(3, 'PRINCIPAL', 0.73, 0.57),
        ROW(3, 'PRINCIPAL', 0.27, 0.57),
        ROW(3, 'PRINCIPAL', 0.17, 0.83),
        ROW(3, 'PRINCIPAL', 0.55, 0.57),
        ROW(3, 'PRINCIPAL', 0.45, 0.83),
        ROW(3, 'ADVERSARIO', 0.17, 0.17),
        ROW(3, 'ADVERSARIO', 0.27, 0.43),
        ROW(3, 'ADVERSARIO', 0.73, 0.43),
        ROW(3, 'ADVERSARIO', 0.83, 0.17),
        ROW(3, 'ADVERSARIO', 0.45, 0.43),
        ROW(3, 'ADVERSARIO', 0.55, 0.17)
),
fases_a_girar AS (
    SELECT q.id AS quadro_id, f.modelo, f.bola_x, f.bola_y
    FROM taticas_demo AS d
    JOIN tatica AS t ON t.id = d.id AND t.equipe_id = d.equipe_id AND t.nome = d.nome
    JOIN fases_demo AS f ON f.tatica_id = t.id
    JOIN quadro AS q ON q.tatica_id = t.id AND q.nome = f.nome
    JOIN bola_quadro AS b ON b.quadro_id = q.id AND b.posicao_x = f.bola_x AND b.posicao_y = f.bola_y
    JOIN atleta_quadro AS a ON a.quadro_id = q.id
    LEFT JOIN formacoes_antigas AS m ON m.modelo = f.modelo AND m.lado = a.lado
        AND m.x = a.posicao_x AND m.y = a.posicao_y
    GROUP BY q.id, f.modelo, f.bola_x, f.bola_y
    HAVING COUNT(*) = 12 AND COUNT(DISTINCT m.lado, m.x, m.y) = 12
)
-- O mesmo giro de 90° leva o time principal para a esquerda e mantém a trajetória da bola.
-- Os dois eixos vêm do modelo original. Atletas e bola mudam em um único UPDATE.
UPDATE /*+ SET_VAR(sql_safe_updates=0) */ atleta_quadro AS a
JOIN fases_a_girar AS f ON f.quadro_id = a.quadro_id
JOIN formacoes_antigas AS m ON m.modelo = f.modelo AND m.lado = a.lado
    AND m.x = a.posicao_x AND m.y = a.posicao_y
JOIN bola_quadro AS b ON b.quadro_id = f.quadro_id
    AND b.posicao_x = f.bola_x AND b.posicao_y = f.bola_y
SET a.posicao_x = 1 - m.y,
    a.posicao_y = m.x,
    b.posicao_x = 1 - f.bola_y,
    b.posicao_y = f.bola_x;

SELECT ROW_COUNT() AS registros_corrigidos;
