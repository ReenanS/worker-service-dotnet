-- Usar a base de dados criada
USE ConsorcioDB;
GO

-- Criação da tabela de Grupos de Consórcio
CREATE TABLE GruposConsorcio (
    ID_Grupo INT PRIMARY KEY,
    PZ_Comercializacao INT,
    CD_Grupo VARCHAR(10) NOT NULL,
    NO_Maximo_Cota INT,
    ST_Situacao CHAR(1), -- 'A' para Ativo, 'I' para Inativo
    NM_Situacao VARCHAR(20), -- Descrição do status
    ID_Produto INT,
    Dia_Vencimento INT
);
GO