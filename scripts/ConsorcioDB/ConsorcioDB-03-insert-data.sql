-- Usar a base de dados criada
USE ConsorcioDB;
GO

-- Inserção de dados na tabela de Grupos de Consórcio
INSERT INTO GruposConsorcio 
    (ID_Grupo, PZ_Comercializacao, CD_Grupo, NO_Maximo_Cota, ST_Situacao, NM_Situacao, ID_Produto, Dia_Vencimento)
VALUES 
    (1, 12, 'G001', 100, 'A', 'Ativo', 100, 15),
    (2, 24, 'G002', 50, 'I', 'Inativo', 134, 30),
    (3, 36, 'G003', 75, 'A', 'Ativo', 135, 10),
    (4, 48, 'G004', 200, 'A', 'Ativo', 136, 5),
    (5, 60, 'G005', 150, 'I', 'Inativo', 137, 20),
    (6, 72, 'G006', 300, 'A', 'Ativo', 135, 25),
    (7, 84, 'G007', 400, 'I', 'Inativo', 136, 10),
    (8, 96, 'G008', 500, 'A', 'Ativo', 137, 5),
    (9, 108, 'G009', 600, 'I', 'Inativo', 100, 20),
    (10, 120, 'G010', 700, 'A', 'Ativo', 134, 30);
GO
