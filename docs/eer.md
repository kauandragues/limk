# EER do banco de dados do LIMK

Esta representação física foi produzida a partir das 15 tabelas e das chaves estrangeiras de [limk-completo.sql](../banco/limk-completo.sql). O [modelo conceitual](mer.md) apresenta o domínio da aplicação.

```mermaid
erDiagram
    USUARIO {
        int id PK
        varchar nome
        varchar email UK
        varchar senha_hash
        datetime criado_em
    }

    PREFERENCIA_USUARIO {
        int usuario_id PK, FK
        char cor_atleta_principal
        char cor_atleta_adversario
        char cor_bola
        int duracao_quadro_padrao_ms
    }

    EQUIPE {
        int id PK
        varchar nome
        mediumblob escudo
    }

    USUARIO_EQUIPE {
        int usuario_id PK, FK
        int equipe_id PK, FK
    }

    ATLETA {
        int id PK
        varchar nome
        varchar apelido
        date data_nascimento
    }

    POSICAO {
        int id PK
        varchar nome UK
    }

    ATLETA_POSICAO {
        int atleta_id PK, FK
        int posicao_id PK, FK
    }

    EQUIPE_ATLETA {
        int id PK
        int equipe_id FK
        int atleta_id FK
        tinyint numero_camisa
        boolean titular
        boolean ativo
    }

    TIPO_TATICA {
        int id PK
        varchar nome UK
    }

    SISTEMA_JOGO {
        int id PK
        varchar nome UK
        varchar descricao
    }

    TATICA {
        int id PK
        int equipe_id FK
        int tipo_tatica_id FK
        int sistema_jogo_id FK
        varchar nome
        text descricao
    }

    QUADRO {
        int id PK
        int tatica_id FK
        varchar nome
        int ordem
        int duracao_ms
        tinyint rotacao
    }

    ATLETA_QUADRO {
        int id PK
        int quadro_id FK
        int equipe_atleta_id FK
        tinyint numero
        enum lado
        decimal posicao_x
        decimal posicao_y
    }

    BOLA_QUADRO {
        int quadro_id PK, FK
        decimal posicao_x
        decimal posicao_y
    }

    TATICA_FAVORITA {
        int usuario_id PK, FK
        int tatica_id PK, FK
        datetime criado_em
    }

    USUARIO ||--o| PREFERENCIA_USUARIO : possui
    USUARIO ||--o{ USUARIO_EQUIPE : acessa
    EQUIPE ||--o{ USUARIO_EQUIPE : compartilhada_com

    ATLETA ||--o{ ATLETA_POSICAO : possui
    POSICAO ||--o{ ATLETA_POSICAO : classifica
    EQUIPE ||--o{ EQUIPE_ATLETA : escala
    ATLETA ||--o{ EQUIPE_ATLETA : integra

    EQUIPE ||--o{ TATICA : organiza
    TIPO_TATICA o|--o{ TATICA : classifica
    SISTEMA_JOGO o|--o{ TATICA : classifica
    TATICA ||--o{ QUADRO : contem
    QUADRO ||--o{ ATLETA_QUADRO : posiciona
    EQUIPE_ATLETA o|--o{ ATLETA_QUADRO : identifica
    QUADRO ||--o| BOLA_QUADRO : posiciona

    USUARIO ||--o{ TATICA_FAVORITA : referencia
    TATICA ||--o{ TATICA_FAVORITA : recebe
```

## Como interpretar

- **PK** identifica a chave primária; **FK**, uma chave estrangeira; **UK**, uma restrição de unicidade.
- `USUARIO_EQUIPE` resolve o relacionamento muitos-para-muitos entre usuários e equipes: uma equipe pode ser compartilhada com vários usuários.
- `EQUIPE_ATLETA` representa a participação de um atleta no elenco de uma equipe. É nessa relação que ficam número da camisa, titularidade e situação ativa. O mesmo atleta pode integrar equipes diferentes.
- `ATLETA_POSICAO` resolve o relacionamento muitos-para-muitos entre atletas e posições.
- `ATLETA_QUADRO` guarda a posição e o número do atleta em um quadro específico. O vínculo opcional com `EQUIPE_ATLETA` permite representar atletas adversários sem cadastro no LIMK.
- `BOLA_QUADRO` tem `quadro_id` como chave primária e estrangeira; assim, cada quadro pode ter zero ou uma bola registrada.
- `TATICA_FAVORITA` contém registros do script de demonstração e referencia usuário e tática. O escopo funcional está em [LIMK_Requisitos.md](LIMK_Requisitos.md).
- O modelo atual não tem tabela separada para projeto: cada tática pertence diretamente a uma equipe.

## Cardinalidades importantes

| Relacionamento | Cardinalidade | Interpretação |
|---|---|---|
| Usuário — Preferência | 1 para 0..1 | Uma conta pode ter uma linha de preferências. |
| Usuário — Equipe | N para N | Usuários acessam equipes por `usuario_equipe`. |
| Equipe — Atleta | N para N | O vínculo e os dados do elenco ficam em `equipe_atleta`. |
| Atleta — Posição | N para N | Um atleta pode ter várias posições; uma posição pode pertencer a vários atletas. |
| Equipe — Tática | 1 para N | Cada tática referencia uma equipe. |
| Tática — Quadro | 1 para 0..N | Uma tática pode conter vários quadros; cada quadro pertence a uma tática. |
| Quadro — Atleta no quadro | 1 para 0..N | Um quadro pode registrar vários atletas posicionados. |
| Quadro — Bola | 1 para 0..1 | Cada quadro pode ter no máximo uma posição de bola. |
| Usuário — Tática favorita | N para N | `tatica_favorita` registra os favoritos. |

As cardinalidades refletem as chaves e restrições do MySQL. No aplicativo, o editor exige pelo menos uma fase para salvar a tática. O [MER conceitual](mer.md) inclui essa regra.
