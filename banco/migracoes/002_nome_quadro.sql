-- Execute uma vez, depois da migracao 001 e de fazer um backup do banco.
-- Preserva os quadros e preenche o novo campo com a ordem ja existente.

USE limk;

ALTER TABLE quadro ADD COLUMN nome VARCHAR(100) NULL AFTER tatica_id;

UPDATE quadro SET nome = CONCAT('Fase ', ordem) WHERE nome IS NULL;

ALTER TABLE quadro MODIFY COLUMN nome VARCHAR(100) NOT NULL;
