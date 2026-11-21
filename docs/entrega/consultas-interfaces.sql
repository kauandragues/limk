-- Consultas da aplicação, agrupadas por finalidade. Executar após a instalação do banco limk.
USE limk;
SET @usuarioId = 1, @equipeId = 1, @atletaId = 1, @taticaId = 1, @email = 'ana@limk.example';

-- 01. Login por e-mail e leitura do hash para autenticação (AutenticacaoUsuario.cs)
SELECT id, nome, email, senha_hash
FROM usuario
WHERE email = @email
LIMIT 1;

-- 02. Início · totais das equipes acessíveis (DadosInicio.cs)
SELECT
(SELECT COUNT(*) FROM usuario_equipe ue
    WHERE ue.usuario_id = @usuarioId) AS equipes,
(SELECT COUNT(DISTINCT ea.atleta_id) FROM equipe_atleta ea
    INNER JOIN usuario_equipe ue ON ue.equipe_id = ea.equipe_id
    WHERE ue.usuario_id = @usuarioId AND ea.ativo = TRUE) AS atletas,
(SELECT COUNT(*) FROM tatica t
    INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
    WHERE ue.usuario_id = @usuarioId) AS taticas,
(SELECT COUNT(*) FROM quadro q
    INNER JOIN tatica t ON t.id = q.tatica_id
    INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
    WHERE ue.usuario_id = @usuarioId) AS quadros;

-- 03. Início · três últimas táticas (DadosInicio.cs)
SELECT t.nome, t.descricao, e.nome AS equipe,
    (SELECT COUNT(*) FROM quadro q WHERE q.tatica_id = t.id) AS quadros, t.id
FROM tatica t
INNER JOIN equipe e ON e.id = t.equipe_id
INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
WHERE ue.usuario_id = @usuarioId
ORDER BY t.id DESC
LIMIT 3;

-- 04. Início · equipes e totais de elenco e táticas (DadosInicio.cs)
SELECT e.nome,
    (SELECT COUNT(*) FROM equipe_atleta ea
        WHERE ea.equipe_id = e.id AND ea.ativo = TRUE) AS atletas,
    (SELECT COUNT(*) FROM tatica t WHERE t.equipe_id = e.id) AS taticas
FROM equipe e
INNER JOIN usuario_equipe ue ON ue.equipe_id = e.id
WHERE ue.usuario_id = @usuarioId
ORDER BY e.nome, e.id
LIMIT 3;

-- 05. Equipes · cartões, escudos e contagens (DadosUsuario.cs)
SELECT e.id, e.nome, e.escudo,
    (SELECT COUNT(*) FROM equipe_atleta ea
        WHERE ea.equipe_id = e.id AND ea.ativo = TRUE) AS atletas,
    (SELECT COUNT(*) FROM tatica t WHERE t.equipe_id = e.id) AS taticas
FROM equipe e
INNER JOIN usuario_equipe ue ON ue.equipe_id = e.id
WHERE ue.usuario_id = @usuarioId
ORDER BY e.nome, e.id;

-- 06. Atletas · elenco ativo, posições e permissão de edição (DadosUsuario.cs)
SELECT a.id, ea.equipe_id, ea.numero_camisa,
    a.nome, a.apelido, a.data_nascimento, e.nome AS equipe, ea.titular,
    (SELECT GROUP_CONCAT(DISTINCT p.nome ORDER BY p.nome SEPARATOR ', ')
        FROM atleta_posicao ap INNER JOIN posicao p ON p.id = ap.posicao_id
        WHERE ap.atleta_id = a.id) AS posicoes,
    (SELECT GROUP_CONCAT(ap.posicao_id ORDER BY ap.posicao_id)
        FROM atleta_posicao ap WHERE ap.atleta_id = a.id) AS posicao_ids,
    NOT EXISTS (SELECT 1 FROM equipe_atleta outro
        LEFT JOIN usuario_equipe acesso ON acesso.equipe_id = outro.equipe_id
            AND acesso.usuario_id = @usuarioId
        WHERE outro.atleta_id = a.id AND acesso.usuario_id IS NULL) AS pode_editar_dados
FROM atleta a
INNER JOIN equipe_atleta ea ON ea.atleta_id = a.id AND ea.ativo = TRUE
INNER JOIN equipe e ON e.id = ea.equipe_id
INNER JOIN usuario_equipe ue ON ue.equipe_id = e.id
WHERE ue.usuario_id = @usuarioId
ORDER BY a.nome, e.nome, a.id, e.id;

-- 07. Táticas · cartões e quantidade de quadros (DadosUsuario.cs)
SELECT t.id, t.equipe_id, t.nome, t.descricao,
    e.nome AS equipe, tt.nome AS tipo, sj.nome AS sistema,
    (SELECT COUNT(*) FROM quadro q WHERE q.tatica_id = t.id) AS quadros
FROM tatica t
INNER JOIN equipe e ON e.id = t.equipe_id
INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
LEFT JOIN tipo_tatica tt ON tt.id = t.tipo_tatica_id
LEFT JOIN sistema_jogo sj ON sj.id = t.sistema_jogo_id
WHERE ue.usuario_id = @usuarioId
ORDER BY t.id DESC;

-- 08. Ficha de atleta · catálogo de posições (DadosAtletas.cs)
SELECT id, nome FROM posicao ORDER BY nome, id;

-- 09. Configurações e prancheta · preferências da conta (DadosPreferenciasUsuario.cs)
SELECT cor_atleta_principal, cor_atleta_adversario, cor_bola, duracao_quadro_padrao_ms
FROM preferencia_usuario
WHERE usuario_id = @usuarioId;

-- 10. Prancheta · tipos de tática (DadosPrancheta.cs)
SELECT id, nome FROM tipo_tatica ORDER BY nome;

-- 11. Prancheta · sistemas de jogo (DadosPrancheta.cs)
SELECT id, nome FROM sistema_jogo ORDER BY nome;

-- 12. Prancheta · elenco ativo e acesso à equipe (DadosPrancheta.cs)
SELECT ea.id, a.nome, ea.numero_camisa
FROM usuario_equipe ue
LEFT JOIN equipe_atleta ea ON ea.equipe_id = ue.equipe_id AND ea.ativo = TRUE
LEFT JOIN atleta a ON a.id = ea.atleta_id
WHERE ue.usuario_id = @usuarioId AND ue.equipe_id = @equipeId
ORDER BY ea.numero_camisa, a.nome;

-- 13. Abrir tática · cabeçalho da jogada (DadosPrancheta.cs)
SELECT t.id, t.equipe_id, t.nome, t.descricao, t.tipo_tatica_id, t.sistema_jogo_id
FROM tatica t INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
WHERE t.id = @taticaId AND ue.usuario_id = @usuarioId;

-- 14. Abrir tática · quadros em ordem (DadosPrancheta.cs)
SELECT q.id, q.nome, q.duracao_ms, q.rotacao
FROM quadro q INNER JOIN tatica t ON t.id = q.tatica_id
INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
WHERE t.id = @taticaId AND ue.usuario_id = @usuarioId ORDER BY q.ordem;

-- 15. Abrir tática · atletas e posições por quadro (DadosPrancheta.cs)
SELECT aq.quadro_id, ea.id, a.nome, aq.numero, aq.lado, aq.posicao_x, aq.posicao_y
FROM atleta_quadro aq INNER JOIN quadro q ON q.id = aq.quadro_id
INNER JOIN tatica t ON t.id = q.tatica_id
INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
LEFT JOIN equipe_atleta ea ON ea.id = aq.equipe_atleta_id AND ea.equipe_id = t.equipe_id
LEFT JOIN atleta a ON a.id = ea.atleta_id
WHERE t.id = @taticaId AND ue.usuario_id = @usuarioId ORDER BY q.ordem, aq.lado, aq.numero;

-- 16. Abrir tática · posição da bola por quadro (DadosPrancheta.cs)
SELECT b.quadro_id, b.posicao_x, b.posicao_y
FROM bola_quadro b INNER JOIN quadro q ON q.id = b.quadro_id
INNER JOIN tatica t ON t.id = q.tatica_id
INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
WHERE t.id = @taticaId AND ue.usuario_id = @usuarioId;

-- Na aplicação estas leituras integram a transação de sua operação de escrita.
START TRANSACTION;

-- 17. Editar ou excluir equipe · bloqueio exclusivo da equipe (DadosEquipe.cs)
SELECT id FROM equipe WHERE id=@equipeId FOR UPDATE;

-- 18. Editar elenco ou salvar tática · bloqueio compartilhado da equipe (DadosEquipe.cs)
SELECT id FROM equipe WHERE id=@equipeId FOR SHARE;

-- 19. Equipe · verificar o vínculo de acesso do usuário (DadosEquipe.cs)
SELECT equipe_id FROM usuario_equipe WHERE usuario_id=@usuarioId AND equipe_id=@equipeId FOR SHARE;

-- 20. Excluir equipe · localizar cadastros de atletas a verificar (DadosEquipe.cs)
SELECT a.id FROM atleta a
INNER JOIN equipe_atleta ea ON ea.atleta_id=a.id
WHERE ea.equipe_id=@equipeId ORDER BY a.id FOR UPDATE OF a;

-- 21. Editar atleta · bloquear cadastro (DadosAtletas.cs)
SELECT id FROM atleta WHERE id = @atletaId FOR UPDATE;

-- 22. Editar atleta · conferir vínculos e permissões (DadosAtletas.cs)
SELECT ea.equipe_id, ea.ativo, ue.usuario_id
FROM equipe_atleta ea
LEFT JOIN usuario_equipe ue ON ue.equipe_id = ea.equipe_id AND ue.usuario_id = @usuarioId
WHERE ea.atleta_id = @atletaId FOR UPDATE OF ea FOR SHARE OF ue;

-- 23. Salvar tática existente · validar equipe e acesso (DadosPrancheta.cs)
SELECT t.equipe_id FROM tatica t
INNER JOIN usuario_equipe ue ON ue.equipe_id=t.equipe_id
WHERE t.id=@taticaId AND ue.usuario_id=@usuarioId FOR UPDATE OF t FOR SHARE OF ue;

-- 24. Salvar tática · conferir vínculos do elenco (DadosPrancheta.cs)
SELECT id FROM equipe_atleta WHERE equipe_id=@equipeId FOR SHARE;

-- Libera os bloqueios obtidos pela demonstração.
ROLLBACK;
