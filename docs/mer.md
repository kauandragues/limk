# Modelo conceitual do LIMK Volleyball

O usuário administra equipes. Cada equipe organiza o elenco e suas táticas. Uma tática é apresentada como uma sequência de fases, nas quais o treinador posiciona os atletas e, se desejar, a bola.

```mermaid
erDiagram
    USUARIO }o..o{ EQUIPE : administra
    EQUIPE ||..o{ VINCULO_ELENCO : possui
    ATLETA ||..o{ VINCULO_ELENCO : integra
    ATLETA }o..o{ POSICAO : exerce
    EQUIPE ||..o{ TATICA : prepara
    TATICA ||--|{ FASE : organiza
    FASE ||--o{ ATLETA_NA_FASE : posiciona
    VINCULO_ELENCO o|..o{ ATLETA_NA_FASE : identifica
    FASE ||--o| BOLA_NA_FASE : posiciona
    VINCULO_ELENCO {
        numero camisa
        booleano titular
        booleano ativo
    }
    TATICA {
        texto nome
        texto descricao
    }
    FASE {
        texto nome
        numero ordem
        numero duracao
        numero rotacao
    }
    ATLETA_NA_FASE {
        numero camisa_registrada
        texto lado
        numero x
        numero y
    }
    BOLA_NA_FASE {
        numero x
        numero y
    }
```

O vínculo de elenco explica por que um atleta pode usar camisas diferentes em duas equipes. Um atleta desenhado na fase pode ser genérico. Quando vem do elenco, a fase conserva o número daquela cena, mesmo que a camisa da equipe mude depois.

A multiplicidade de uma ou mais fases expressa a regra de salvamento da aplicação. No MySQL, cada quadro referencia uma tática, mas a chave estrangeira sozinha permite uma tática sem quadros. O [diagrama do esquema físico](eer.md) detalha as 15 tabelas.
