CREATE DATABASE limk;
USE limk;

CREATE TABLE usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(254) NOT NULL UNIQUE,
    senha_hash VARCHAR(255) NOT NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE preferencia_usuario (
    usuario_id INT PRIMARY KEY,
    cor_atleta_principal CHAR(7) NOT NULL DEFAULT '#6935D3',
    cor_atleta_adversario CHAR(7) NOT NULL DEFAULT '#E53935',
    cor_bola CHAR(7) NOT NULL DEFAULT '#FFD700',
    duracao_quadro_padrao_ms INT NOT NULL DEFAULT 1000,
    FOREIGN KEY (usuario_id) REFERENCES usuario(id) ON DELETE CASCADE,
    CHECK (duracao_quadro_padrao_ms > 0)
);

CREATE TABLE equipe (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    escudo MEDIUMBLOB NULL
);

CREATE TABLE usuario_equipe (
    usuario_id INT NOT NULL,
    equipe_id INT NOT NULL,
    PRIMARY KEY (usuario_id, equipe_id),
    FOREIGN KEY (usuario_id) REFERENCES usuario(id) ON DELETE CASCADE,
    FOREIGN KEY (equipe_id) REFERENCES equipe(id) ON DELETE CASCADE
);

CREATE TABLE atleta (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    apelido VARCHAR(50),
    data_nascimento DATE
);

CREATE TABLE posicao (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE atleta_posicao (
    atleta_id INT NOT NULL,
    posicao_id INT NOT NULL,
    PRIMARY KEY (atleta_id, posicao_id),
    FOREIGN KEY (atleta_id) REFERENCES atleta(id) ON DELETE CASCADE,
    FOREIGN KEY (posicao_id) REFERENCES posicao(id) ON DELETE RESTRICT
);

CREATE TABLE equipe_atleta (
    id INT AUTO_INCREMENT PRIMARY KEY,
    equipe_id INT NOT NULL,
    atleta_id INT NOT NULL,
    numero_camisa TINYINT UNSIGNED NOT NULL,
    titular BOOLEAN NOT NULL DEFAULT FALSE,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    UNIQUE (equipe_id, atleta_id),
    UNIQUE (equipe_id, numero_camisa),
    FOREIGN KEY (equipe_id) REFERENCES equipe(id) ON DELETE CASCADE,
    FOREIGN KEY (atleta_id) REFERENCES atleta(id) ON DELETE RESTRICT,
    CHECK (numero_camisa BETWEEN 1 AND 99)
);

CREATE TABLE tipo_tatica (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE sistema_jogo (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(20) NOT NULL UNIQUE,
    descricao VARCHAR(255)
);

CREATE TABLE tatica (
    id INT AUTO_INCREMENT PRIMARY KEY,
    equipe_id INT NOT NULL,
    tipo_tatica_id INT,
    sistema_jogo_id INT,
    nome VARCHAR(100) NOT NULL,
    descricao TEXT,
    FOREIGN KEY (equipe_id) REFERENCES equipe(id) ON DELETE CASCADE,
    FOREIGN KEY (tipo_tatica_id) REFERENCES tipo_tatica(id) ON DELETE SET NULL,
    FOREIGN KEY (sistema_jogo_id) REFERENCES sistema_jogo(id) ON DELETE SET NULL
);

CREATE TABLE quadro (
    id INT AUTO_INCREMENT PRIMARY KEY,
    tatica_id INT NOT NULL,
    nome VARCHAR(100) NOT NULL,
    ordem INT NOT NULL,
    duracao_ms INT NOT NULL DEFAULT 1000,
    rotacao TINYINT UNSIGNED,
    UNIQUE (tatica_id, ordem),
    FOREIGN KEY (tatica_id) REFERENCES tatica(id) ON DELETE CASCADE,
    CHECK (ordem >= 1),
    CHECK (duracao_ms > 0),
    CHECK (rotacao IS NULL OR rotacao BETWEEN 1 AND 6)
);

CREATE TABLE atleta_quadro (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quadro_id INT NOT NULL,
    equipe_atleta_id INT,
    numero TINYINT UNSIGNED NOT NULL,
    lado ENUM('PRINCIPAL', 'ADVERSARIO') NOT NULL DEFAULT 'PRINCIPAL',
    posicao_x DECIMAL(9,8) NOT NULL,
    posicao_y DECIMAL(9,8) NOT NULL,
    UNIQUE (quadro_id, lado, numero),
    UNIQUE (quadro_id, equipe_atleta_id),
    FOREIGN KEY (quadro_id) REFERENCES quadro(id) ON DELETE CASCADE,
    FOREIGN KEY (equipe_atleta_id) REFERENCES equipe_atleta(id) ON DELETE SET NULL,
    CHECK (numero BETWEEN 1 AND 99),
    CHECK (posicao_x BETWEEN 0 AND 1),
    CHECK (posicao_y BETWEEN 0 AND 1)
);

CREATE TABLE bola_quadro (
    quadro_id INT PRIMARY KEY,
    posicao_x DECIMAL(9,8) NOT NULL,
    posicao_y DECIMAL(9,8) NOT NULL,
    FOREIGN KEY (quadro_id) REFERENCES quadro(id) ON DELETE CASCADE,
    CHECK (posicao_x BETWEEN 0 AND 1),
    CHECK (posicao_y BETWEEN 0 AND 1)
);

CREATE TABLE tatica_favorita (
    usuario_id INT NOT NULL,
    tatica_id INT NOT NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (usuario_id, tatica_id),
    FOREIGN KEY (usuario_id) REFERENCES usuario(id) ON DELETE CASCADE,
    FOREIGN KEY (tatica_id) REFERENCES tatica(id) ON DELETE CASCADE
);

INSERT INTO usuario (id, nome, email, senha_hash) VALUES (1, 'Ana Lima', 'ana@limk.example', '$2a$12$Kt3qTUbIJ1/UZW9NfchKdug/OYWTAwpKCar0Bl1Et1CoBNIlgVl1O');
INSERT INTO usuario (id, nome, email, senha_hash) VALUES (2, 'Bruno Alves', 'bruno@limk.example', '$2a$12$gwvYYMqLKmnuWzu4iBIoz.IkwvvshsJTFVh.kKAP.W05NgpbEZtae');
INSERT INTO usuario (id, nome, email, senha_hash) VALUES (3, 'Carla Mendes', 'carla@limk.example', '$2a$12$p7jWkJ8Hx9od3y7n/Wea1OpZUdmbSRkcNgjoA0gu4XvnLufVkhUq6');
INSERT INTO preferencia_usuario (usuario_id, cor_atleta_principal, cor_atleta_adversario, cor_bola, duracao_quadro_padrao_ms) VALUES (1, '#6935D3', '#E53935', '#FFD700', 1000);
INSERT INTO preferencia_usuario (usuario_id, cor_atleta_principal, cor_atleta_adversario, cor_bola, duracao_quadro_padrao_ms) VALUES (2, '#1565C0', '#E53935', '#FFD700', 1500);
INSERT INTO preferencia_usuario (usuario_id, cor_atleta_principal, cor_atleta_adversario, cor_bola, duracao_quadro_padrao_ms) VALUES (3, '#00897B', '#E53935', '#FFD700', 1200);
INSERT INTO equipe (id, nome, escudo) VALUES (1, 'LIMK Campinas', NULL);
INSERT INTO equipe (id, nome, escudo) VALUES (2, 'Vôlei Hortolândia', NULL);
INSERT INTO usuario_equipe (usuario_id, equipe_id) VALUES (1, 1);
INSERT INTO usuario_equipe (usuario_id, equipe_id) VALUES (2, 1);
INSERT INTO usuario_equipe (usuario_id, equipe_id) VALUES (3, 2);
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (1, 'Lucas Almeida', 'Lucas', '2001-02-02');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (2, 'Pedro Santos', 'Pedro', '2002-03-03');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (3, 'Gabriel Costa', 'Gabi', '2003-04-04');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (4, 'Rafael Oliveira', 'Rafa', '2004-05-05');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (5, 'Mateus Souza', 'Teus', '2000-06-06');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (6, 'Felipe Rocha', 'Fê', '2001-07-07');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (7, 'Diego Martins', 'Diego', '2002-08-08');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (8, 'André Ferreira', 'Dedé', '2003-09-09');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (9, 'Mariana Ribeiro', 'Mari', '2004-10-10');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (10, 'Beatriz Gomes', 'Bia', '2000-11-11');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (11, 'Juliana Castro', 'Ju', '2001-12-12');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (12, 'Camila Moreira', 'Cami', '2002-01-13');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (13, 'Fernanda Dias', 'Fê', '2003-02-14');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (14, 'Letícia Barbosa', 'Lê', '2004-03-15');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (15, 'Patrícia Nunes', 'Paty', '2000-04-16');
INSERT INTO atleta (id, nome, apelido, data_nascimento) VALUES (16, 'Renata Azevedo', 'Rê', '2001-05-17');
INSERT INTO posicao (id, nome) VALUES (1, 'Levantador');
INSERT INTO posicao (id, nome) VALUES (2, 'Oposto');
INSERT INTO posicao (id, nome) VALUES (3, 'Ponteiro');
INSERT INTO posicao (id, nome) VALUES (4, 'Central');
INSERT INTO posicao (id, nome) VALUES (5, 'Líbero');
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (1, 1);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (2, 2);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (3, 3);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (4, 3);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (5, 4);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (6, 4);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (7, 5);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (8, 3);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (9, 1);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (10, 2);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (11, 3);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (12, 3);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (13, 4);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (14, 4);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (15, 5);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (16, 3);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (8, 2);
INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (16, 2);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (1, 1, 1, 4, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (2, 1, 2, 8, FALSE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (3, 1, 3, 10, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (4, 1, 4, 1, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (5, 1, 5, 5, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (6, 1, 6, 11, FALSE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (7, 1, 7, 3, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (8, 1, 8, 2, FALSE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (9, 2, 9, 1, FALSE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (10, 2, 10, 2, FALSE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (11, 2, 11, 3, FALSE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (12, 2, 12, 4, FALSE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (13, 2, 13, 13, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (14, 2, 14, 14, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (15, 2, 15, 20, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (16, 2, 16, 99, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (17, 1, 9, 6, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (18, 1, 10, 7, TRUE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (19, 1, 11, 9, FALSE, TRUE);
INSERT INTO equipe_atleta (id, equipe_id, atleta_id, numero_camisa, titular, ativo) VALUES (20, 1, 12, 12, FALSE, TRUE);
INSERT INTO tipo_tatica (id, nome) VALUES (1, 'Saque');
INSERT INTO tipo_tatica (id, nome) VALUES (2, 'Recepção');
INSERT INTO tipo_tatica (id, nome) VALUES (3, 'Ataque');
INSERT INTO tipo_tatica (id, nome) VALUES (4, 'Defesa');
INSERT INTO tipo_tatica (id, nome) VALUES (5, 'Bloqueio');
INSERT INTO tipo_tatica (id, nome) VALUES (6, 'Cobertura');
INSERT INTO sistema_jogo (id, nome, descricao) VALUES (1, '5-1', 'Um levantador organiza as jogadas nas seis rotações.');
INSERT INTO sistema_jogo (id, nome, descricao) VALUES (2, '4-2', 'Dois levantadores e quatro atacantes.');
INSERT INTO sistema_jogo (id, nome, descricao) VALUES (3, '6-0', 'Sem levantador fixo; organização conforme o rodízio.');
INSERT INTO tatica (id, equipe_id, tipo_tatica_id, sistema_jogo_id, nome, descricao) VALUES (1, 1, 2, 1, 'Recepção inicial', 'Exemplo visual de recepção, ajuste e preparação para o levantamento.');
INSERT INTO tatica (id, equipe_id, tipo_tatica_id, sistema_jogo_id, nome, descricao) VALUES (2, 1, 3, 1, 'Ataque pela entrada', 'Exemplo de aproximação de ataque pela entrada de rede.');
INSERT INTO tatica (id, equipe_id, tipo_tatica_id, sistema_jogo_id, nome, descricao) VALUES (3, 2, 4, 2, 'Organização defensiva', 'Exemplo de deslocamento defensivo após ataque adversário.');
INSERT INTO tatica (id, equipe_id, tipo_tatica_id, sistema_jogo_id, nome, descricao) VALUES (4, 2, 6, 2, 'Cobertura do ataque', 'Exemplo de aproximação para cobertura do atacante.');

INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (1, 1, 'Recepção inicial', 1, 1000, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (1, 1, 1, 4, 'PRINCIPAL', 0.12, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (2, 1, 2, 8, 'PRINCIPAL', 0.38, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (3, 1, 3, 10, 'PRINCIPAL', 0.38, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (4, 1, 4, 1, 'PRINCIPAL', 0.12, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (5, 1, 5, 5, 'PRINCIPAL', 0.38, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (6, 1, 6, 11, 'PRINCIPAL', 0.12, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (7, 1, NULL, 1, 'ADVERSARIO', 0.88, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (8, 1, NULL, 2, 'ADVERSARIO', 0.62, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (9, 1, NULL, 3, 'ADVERSARIO', 0.62, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (10, 1, NULL, 4, 'ADVERSARIO', 0.88, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (11, 1, NULL, 5, 'ADVERSARIO', 0.62, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (12, 1, NULL, 6, 'ADVERSARIO', 0.88, 0.5);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (1, 0.82, 0.5);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (2, 1, 'Ajuste dos passadores', 2, 1000, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (13, 2, 1, 4, 'PRINCIPAL', 0.145, 0.805);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (14, 2, 2, 8, 'PRINCIPAL', 0.405, 0.755);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (15, 2, 3, 10, 'PRINCIPAL', 0.405, 0.245);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (16, 2, 4, 1, 'PRINCIPAL', 0.145, 0.195);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (17, 2, 5, 5, 'PRINCIPAL', 0.405, 0.525);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (18, 2, 6, 11, 'PRINCIPAL', 0.145, 0.475);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (19, 2, NULL, 1, 'ADVERSARIO', 0.855, 0.195);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (20, 2, NULL, 2, 'ADVERSARIO', 0.595, 0.245);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (21, 2, NULL, 3, 'ADVERSARIO', 0.595, 0.755);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (22, 2, NULL, 4, 'ADVERSARIO', 0.855, 0.805);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (23, 2, NULL, 5, 'ADVERSARIO', 0.595, 0.475);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (24, 2, NULL, 6, 'ADVERSARIO', 0.855, 0.525);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (2, 0.18, 0.38);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (3, 1, 'Preparação para levantamento', 3, 1000, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (25, 3, 1, 4, 'PRINCIPAL', 0.17, 0.83);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (26, 3, 2, 8, 'PRINCIPAL', 0.43, 0.73);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (27, 3, 3, 10, 'PRINCIPAL', 0.43, 0.27);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (28, 3, 4, 1, 'PRINCIPAL', 0.17, 0.17);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (29, 3, 5, 5, 'PRINCIPAL', 0.43, 0.55);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (30, 3, 6, 11, 'PRINCIPAL', 0.17, 0.45);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (31, 3, NULL, 1, 'ADVERSARIO', 0.83, 0.17);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (32, 3, NULL, 2, 'ADVERSARIO', 0.57, 0.27);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (33, 3, NULL, 3, 'ADVERSARIO', 0.57, 0.73);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (34, 3, NULL, 4, 'ADVERSARIO', 0.83, 0.83);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (35, 3, NULL, 5, 'ADVERSARIO', 0.57, 0.45);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (36, 3, NULL, 6, 'ADVERSARIO', 0.83, 0.55);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (3, 0.42, 0.68);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (4, 2, 'Posicionamento inicial', 1, 1000, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (37, 4, 1, 4, 'PRINCIPAL', 0.12, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (38, 4, 2, 8, 'PRINCIPAL', 0.38, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (39, 4, 3, 10, 'PRINCIPAL', 0.38, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (40, 4, 4, 1, 'PRINCIPAL', 0.12, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (41, 4, 5, 5, 'PRINCIPAL', 0.38, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (42, 4, 6, 11, 'PRINCIPAL', 0.12, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (43, 4, NULL, 1, 'ADVERSARIO', 0.88, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (44, 4, NULL, 2, 'ADVERSARIO', 0.62, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (45, 4, NULL, 3, 'ADVERSARIO', 0.62, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (46, 4, NULL, 4, 'ADVERSARIO', 0.88, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (47, 4, NULL, 5, 'ADVERSARIO', 0.62, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (48, 4, NULL, 6, 'ADVERSARIO', 0.88, 0.5);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (4, 0.42, 0.68);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (5, 2, 'Aproximação do ataque', 2, 1000, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (49, 5, 1, 4, 'PRINCIPAL', 0.145, 0.805);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (50, 5, 2, 8, 'PRINCIPAL', 0.405, 0.755);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (51, 5, 3, 10, 'PRINCIPAL', 0.405, 0.245);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (52, 5, 4, 1, 'PRINCIPAL', 0.145, 0.195);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (53, 5, 5, 5, 'PRINCIPAL', 0.405, 0.525);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (54, 5, 6, 11, 'PRINCIPAL', 0.145, 0.475);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (55, 5, NULL, 1, 'ADVERSARIO', 0.855, 0.195);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (56, 5, NULL, 2, 'ADVERSARIO', 0.595, 0.245);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (57, 5, NULL, 3, 'ADVERSARIO', 0.595, 0.755);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (58, 5, NULL, 4, 'ADVERSARIO', 0.855, 0.805);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (59, 5, NULL, 5, 'ADVERSARIO', 0.595, 0.475);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (60, 5, NULL, 6, 'ADVERSARIO', 0.855, 0.525);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (5, 0.44, 0.22);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (6, 2, 'Finalização pela entrada', 3, 1000, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (61, 6, 1, 4, 'PRINCIPAL', 0.17, 0.83);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (62, 6, 2, 8, 'PRINCIPAL', 0.43, 0.73);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (63, 6, 3, 10, 'PRINCIPAL', 0.43, 0.27);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (64, 6, 4, 1, 'PRINCIPAL', 0.17, 0.17);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (65, 6, 5, 5, 'PRINCIPAL', 0.43, 0.55);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (66, 6, 6, 11, 'PRINCIPAL', 0.17, 0.45);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (67, 6, NULL, 1, 'ADVERSARIO', 0.83, 0.17);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (68, 6, NULL, 2, 'ADVERSARIO', 0.57, 0.27);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (69, 6, NULL, 3, 'ADVERSARIO', 0.57, 0.73);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (70, 6, NULL, 4, 'ADVERSARIO', 0.83, 0.83);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (71, 6, NULL, 5, 'ADVERSARIO', 0.57, 0.45);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (72, 6, NULL, 6, 'ADVERSARIO', 0.83, 0.55);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (6, 0.78, 0.3);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (7, 3, 'Organização defensiva', 1, 1200, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (73, 7, 9, 1, 'PRINCIPAL', 0.12, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (74, 7, 10, 2, 'PRINCIPAL', 0.38, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (75, 7, 11, 3, 'PRINCIPAL', 0.38, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (76, 7, 12, 4, 'PRINCIPAL', 0.12, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (77, 7, 13, 13, 'PRINCIPAL', 0.38, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (78, 7, 14, 14, 'PRINCIPAL', 0.12, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (79, 7, NULL, 1, 'ADVERSARIO', 0.88, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (80, 7, NULL, 2, 'ADVERSARIO', 0.62, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (81, 7, NULL, 3, 'ADVERSARIO', 0.62, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (82, 7, NULL, 4, 'ADVERSARIO', 0.88, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (83, 7, NULL, 5, 'ADVERSARIO', 0.62, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (84, 7, NULL, 6, 'ADVERSARIO', 0.88, 0.5);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (7, 0.8, 0.25);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (8, 3, 'Ajuste da defesa', 2, 1200, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (85, 8, 9, 1, 'PRINCIPAL', 0.145, 0.805);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (86, 8, 10, 2, 'PRINCIPAL', 0.405, 0.755);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (87, 8, 11, 3, 'PRINCIPAL', 0.405, 0.245);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (88, 8, 12, 4, 'PRINCIPAL', 0.145, 0.195);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (89, 8, 13, 13, 'PRINCIPAL', 0.405, 0.525);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (90, 8, 14, 14, 'PRINCIPAL', 0.145, 0.475);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (91, 8, NULL, 1, 'ADVERSARIO', 0.855, 0.195);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (92, 8, NULL, 2, 'ADVERSARIO', 0.595, 0.245);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (93, 8, NULL, 3, 'ADVERSARIO', 0.595, 0.755);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (94, 8, NULL, 4, 'ADVERSARIO', 0.855, 0.805);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (95, 8, NULL, 5, 'ADVERSARIO', 0.595, 0.475);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (96, 8, NULL, 6, 'ADVERSARIO', 0.855, 0.525);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (8, 0.17, 0.72);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (9, 3, 'Defesa do ataque', 3, 1200, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (97, 9, 9, 1, 'PRINCIPAL', 0.17, 0.83);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (98, 9, 10, 2, 'PRINCIPAL', 0.43, 0.73);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (99, 9, 11, 3, 'PRINCIPAL', 0.43, 0.27);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (100, 9, 12, 4, 'PRINCIPAL', 0.17, 0.17);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (101, 9, 13, 13, 'PRINCIPAL', 0.43, 0.55);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (102, 9, 14, 14, 'PRINCIPAL', 0.17, 0.45);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (103, 9, NULL, 1, 'ADVERSARIO', 0.83, 0.17);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (104, 9, NULL, 2, 'ADVERSARIO', 0.57, 0.27);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (105, 9, NULL, 3, 'ADVERSARIO', 0.57, 0.73);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (106, 9, NULL, 4, 'ADVERSARIO', 0.83, 0.83);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (107, 9, NULL, 5, 'ADVERSARIO', 0.57, 0.45);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (108, 9, NULL, 6, 'ADVERSARIO', 0.83, 0.55);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (9, 0.4, 0.62);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (10, 4, 'Posicionamento de cobertura', 1, 1200, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (109, 10, 9, 1, 'PRINCIPAL', 0.12, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (110, 10, 10, 2, 'PRINCIPAL', 0.38, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (111, 10, 11, 3, 'PRINCIPAL', 0.38, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (112, 10, 12, 4, 'PRINCIPAL', 0.12, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (113, 10, 13, 13, 'PRINCIPAL', 0.38, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (114, 10, 14, 14, 'PRINCIPAL', 0.12, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (115, 10, NULL, 1, 'ADVERSARIO', 0.88, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (116, 10, NULL, 2, 'ADVERSARIO', 0.62, 0.22);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (117, 10, NULL, 3, 'ADVERSARIO', 0.62, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (118, 10, NULL, 4, 'ADVERSARIO', 0.88, 0.78);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (119, 10, NULL, 5, 'ADVERSARIO', 0.62, 0.5);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (120, 10, NULL, 6, 'ADVERSARIO', 0.88, 0.5);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (10, 0.44, 0.22);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (11, 4, 'Aproximação para cobertura', 2, 1200, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (121, 11, 9, 1, 'PRINCIPAL', 0.145, 0.805);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (122, 11, 10, 2, 'PRINCIPAL', 0.405, 0.755);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (123, 11, 11, 3, 'PRINCIPAL', 0.405, 0.245);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (124, 11, 12, 4, 'PRINCIPAL', 0.145, 0.195);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (125, 11, 13, 13, 'PRINCIPAL', 0.405, 0.525);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (126, 11, 14, 14, 'PRINCIPAL', 0.145, 0.475);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (127, 11, NULL, 1, 'ADVERSARIO', 0.855, 0.195);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (128, 11, NULL, 2, 'ADVERSARIO', 0.595, 0.245);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (129, 11, NULL, 3, 'ADVERSARIO', 0.595, 0.755);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (130, 11, NULL, 4, 'ADVERSARIO', 0.855, 0.805);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (131, 11, NULL, 5, 'ADVERSARIO', 0.595, 0.475);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (132, 11, NULL, 6, 'ADVERSARIO', 0.855, 0.525);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (11, 0.49, 0.22);
INSERT INTO quadro (id, tatica_id, nome, ordem, duracao_ms, rotacao) VALUES (12, 4, 'Cobertura do atacante', 3, 1200, 1);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (133, 12, 9, 1, 'PRINCIPAL', 0.17, 0.83);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (134, 12, 10, 2, 'PRINCIPAL', 0.43, 0.73);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (135, 12, 11, 3, 'PRINCIPAL', 0.43, 0.27);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (136, 12, 12, 4, 'PRINCIPAL', 0.17, 0.17);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (137, 12, 13, 13, 'PRINCIPAL', 0.43, 0.55);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (138, 12, 14, 14, 'PRINCIPAL', 0.17, 0.45);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (139, 12, NULL, 1, 'ADVERSARIO', 0.83, 0.17);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (140, 12, NULL, 2, 'ADVERSARIO', 0.57, 0.27);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (141, 12, NULL, 3, 'ADVERSARIO', 0.57, 0.73);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (142, 12, NULL, 4, 'ADVERSARIO', 0.83, 0.83);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (143, 12, NULL, 5, 'ADVERSARIO', 0.57, 0.45);
INSERT INTO atleta_quadro (id, quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y) VALUES (144, 12, NULL, 6, 'ADVERSARIO', 0.83, 0.55);
INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (12, 0.34, 0.35);
INSERT INTO tatica_favorita (usuario_id, tatica_id) VALUES (1, 1);
INSERT INTO tatica_favorita (usuario_id, tatica_id) VALUES (1, 2);
INSERT INTO tatica_favorita (usuario_id, tatica_id) VALUES (2, 1);
INSERT INTO tatica_favorita (usuario_id, tatica_id) VALUES (3, 3);

-- Consultas usadas no sistema
-- Valores de exemplo para testar as consultas.
SET @usuarioId = 1,
    @equipeId = 1,
    @atletaId = 1,
    @taticaId = 1,
    @email = 'ana@limk.example';

-- Login pelo e-mail
SELECT id, nome, email, senha_hash
FROM usuario
WHERE email = @email
LIMIT 1;

-- Totais da tela inicial
SELECT
    COUNT(DISTINCT usuario_equipe.equipe_id) AS equipes,
    COUNT(DISTINCT equipe_atleta.atleta_id) AS atletas,
    COUNT(DISTINCT tatica.id) AS taticas,
    COUNT(DISTINCT quadro.id) AS quadros
FROM usuario_equipe
LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = usuario_equipe.equipe_id AND equipe_atleta.ativo = TRUE
LEFT JOIN tatica ON tatica.equipe_id = usuario_equipe.equipe_id
LEFT JOIN quadro ON quadro.tatica_id = tatica.id
WHERE usuario_equipe.usuario_id = @usuarioId;

-- Últimas três táticas cadastradas
SELECT tatica.nome, tatica.descricao, equipe.nome AS equipe,
    COUNT(quadro.id) AS quadros, tatica.id
FROM tatica
INNER JOIN equipe ON equipe.id = tatica.equipe_id
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
LEFT JOIN quadro ON quadro.tatica_id = tatica.id
WHERE usuario_equipe.usuario_id = @usuarioId
GROUP BY tatica.id, tatica.nome, tatica.descricao, equipe.nome
ORDER BY tatica.id DESC
LIMIT 3;

-- Equipes exibidas na tela inicial
SELECT equipe.nome,
    COUNT(DISTINCT equipe_atleta.id) AS atletas,
    COUNT(DISTINCT tatica.id) AS taticas
FROM equipe
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe.id
LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = equipe.id AND equipe_atleta.ativo = TRUE
LEFT JOIN tatica ON tatica.equipe_id = equipe.id
WHERE usuario_equipe.usuario_id = @usuarioId
GROUP BY equipe.id, equipe.nome
ORDER BY equipe.nome, equipe.id
LIMIT 3;

-- Equipes do usuário
SELECT equipe.id, equipe.nome, equipe.escudo,
    COUNT(DISTINCT equipe_atleta.id) AS atletas,
    COUNT(DISTINCT tatica.id) AS taticas
FROM equipe
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe.id
LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = equipe.id AND equipe_atleta.ativo = TRUE
LEFT JOIN tatica ON tatica.equipe_id = equipe.id
WHERE usuario_equipe.usuario_id = @usuarioId
GROUP BY equipe.id, equipe.nome, equipe.escudo
ORDER BY equipe.nome, equipe.id;

-- Atletas ativos, posições e permissão de edição
-- O C# junta as posições de cada atleta para mostrar na tela.
SELECT atleta.id, equipe_atleta.equipe_id, equipe_atleta.numero_camisa,
    atleta.nome, atleta.apelido, atleta.data_nascimento, equipe.nome AS equipe, equipe_atleta.titular,
    posicao.nome AS posicoes, posicao.id AS posicao_ids,
    COUNT(DISTINCT vinculo.equipe_id) = COUNT(DISTINCT acesso.equipe_id) AS pode_editar_dados
FROM atleta
INNER JOIN equipe_atleta ON equipe_atleta.atleta_id = atleta.id AND equipe_atleta.ativo = TRUE
INNER JOIN equipe ON equipe.id = equipe_atleta.equipe_id
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe.id
LEFT JOIN atleta_posicao ON atleta_posicao.atleta_id = atleta.id
LEFT JOIN posicao ON posicao.id = atleta_posicao.posicao_id
LEFT JOIN equipe_atleta AS vinculo ON vinculo.atleta_id = atleta.id
LEFT JOIN usuario_equipe AS acesso ON acesso.equipe_id = vinculo.equipe_id AND acesso.usuario_id = @usuarioId
WHERE usuario_equipe.usuario_id = @usuarioId
GROUP BY atleta.id, equipe_atleta.equipe_id, equipe_atleta.numero_camisa,
    atleta.nome, atleta.apelido, atleta.data_nascimento, equipe.nome, equipe_atleta.titular,
    posicao.id, posicao.nome
ORDER BY atleta.nome, equipe.nome, atleta.id, equipe.id, posicao.nome, posicao.id;

-- Posições disponíveis para o cadastro
SELECT id, nome FROM posicao ORDER BY nome, id;

-- Preferências do usuário
SELECT cor_atleta_principal, cor_atleta_adversario, cor_bola, duracao_quadro_padrao_ms
FROM preferencia_usuario
WHERE usuario_id = @usuarioId;

-- Táticas do usuário
SELECT tatica.id, tatica.equipe_id, tatica.nome, tatica.descricao,
    equipe.nome AS equipe, tipo_tatica.nome AS tipo, sistema_jogo.nome AS sistema,
    COUNT(quadro.id) AS quadros
FROM tatica
INNER JOIN equipe ON equipe.id = tatica.equipe_id
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
LEFT JOIN tipo_tatica ON tipo_tatica.id = tatica.tipo_tatica_id
LEFT JOIN sistema_jogo ON sistema_jogo.id = tatica.sistema_jogo_id
LEFT JOIN quadro ON quadro.tatica_id = tatica.id
WHERE usuario_equipe.usuario_id = @usuarioId
GROUP BY tatica.id, tatica.equipe_id, tatica.nome, tatica.descricao,
    equipe.nome, tipo_tatica.nome, sistema_jogo.nome
ORDER BY tatica.id DESC;

-- Tipos de tática
SELECT id, nome FROM tipo_tatica ORDER BY nome;

-- Sistemas de jogo
SELECT id, nome FROM sistema_jogo ORDER BY nome;

-- Elenco da equipe
SELECT equipe_atleta.id, atleta.nome, equipe_atleta.numero_camisa
FROM usuario_equipe
LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = usuario_equipe.equipe_id AND equipe_atleta.ativo = TRUE
LEFT JOIN atleta ON atleta.id = equipe_atleta.atleta_id
WHERE usuario_equipe.usuario_id = @usuarioId AND usuario_equipe.equipe_id = @equipeId
ORDER BY equipe_atleta.numero_camisa, atleta.nome;

-- Dados da tática
SELECT tatica.id, tatica.equipe_id, tatica.nome, tatica.descricao,
    tatica.tipo_tatica_id, tatica.sistema_jogo_id
FROM tatica
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId;

-- Quadros da tática na ordem de reprodução
SELECT quadro.id, quadro.nome, quadro.duracao_ms, quadro.rotacao
FROM quadro
INNER JOIN tatica ON tatica.id = quadro.tatica_id
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId
ORDER BY quadro.ordem;

-- Atletas e posições de cada quadro
SELECT atleta_quadro.quadro_id, equipe_atleta.id, atleta.nome, atleta_quadro.numero,
    atleta_quadro.lado, atleta_quadro.posicao_x, atleta_quadro.posicao_y
FROM atleta_quadro
INNER JOIN quadro ON quadro.id = atleta_quadro.quadro_id
INNER JOIN tatica ON tatica.id = quadro.tatica_id
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
LEFT JOIN equipe_atleta ON equipe_atleta.id = atleta_quadro.equipe_atleta_id AND equipe_atleta.equipe_id = tatica.equipe_id
LEFT JOIN atleta ON atleta.id = equipe_atleta.atleta_id
WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId
ORDER BY quadro.ordem, atleta_quadro.lado, atleta_quadro.numero;

-- Posição da bola em cada quadro
SELECT bola_quadro.quadro_id, bola_quadro.posicao_x, bola_quadro.posicao_y
FROM bola_quadro
INNER JOIN quadro ON quadro.id = bola_quadro.quadro_id
INNER JOIN tatica ON tatica.id = quadro.tatica_id
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId;

-- Confere o acesso do usuário à equipe
SELECT equipe_id FROM usuario_equipe WHERE usuario_id = @usuarioId AND equipe_id = @equipeId;

-- Busca os atletas antes de excluir a equipe
SELECT atleta.id FROM atleta
INNER JOIN equipe_atleta ON equipe_atleta.atleta_id = atleta.id
WHERE equipe_atleta.equipe_id = @equipeId
ORDER BY atleta.id;

-- Confere se o atleta existe
SELECT id FROM atleta WHERE id = @atletaId;

-- Confere os vínculos e as permissões do atleta
SELECT equipe_atleta.equipe_id, equipe_atleta.ativo, usuario_equipe.usuario_id
FROM equipe_atleta
LEFT JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe_atleta.equipe_id AND usuario_equipe.usuario_id = @usuarioId
WHERE equipe_atleta.atleta_id = @atletaId;

-- Confere o acesso à tática e a equipe vinculada
SELECT tatica.equipe_id FROM tatica
INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId;

-- Busca os vínculos do elenco antes de salvar a tática
SELECT id FROM equipe_atleta WHERE equipe_id = @equipeId;
