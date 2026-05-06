# TP2 - Comparacao de algoritmos de ordenacao (C#)

Este trabalho implementa e compara o tempo de execucao em numero de operacoes de 3 algoritmos de ordenacao, com uma escolha em cada classe exigida:

- Classe quadratica: BubbleSort
- Classe O(n log n): QuickSort
- Classe linear media: BucketSort

A comparacao e feita por numero de operacoes (comparacoes e escritas/trocas), conforme o enunciado.

## Formato dos arquivos de entrada

Cada arquivo deve conter:

- Um numero inteiro por linha
- Ultima linha em branco (indicando fim da lista)

## Gerar casos de teste

Dentro da pasta do projeto, execute:

```powershell
dotnet run -- --generate-tests
```

Isso cria a pasta test_data com os arquivos nos tamanhos 100, 1000, 10000 e 100000, nos dois padroes de nomenclatura:

- random_n.txt
- ascending_n.txt
- descending_n.txt
- randomico_n.txt
- ordenado_n.txt
- decrescente_n.txt

Voce pode customizar:

```powershell
dotnet run -- --generate-tests --outdir test_data --sizes 100,1000,10000,100000 --seed 42
```

## Executar comparacao e gerar HTML

No estilo do enunciado, voce pode usar o comando:

```powershell
.\compara.bat test_data/ordenado_100.txt test_data/randomico_1000.txt test_data/decrescente_10000.txt
```

Para executar diretamente com BubbleSort no modo literal:

```powershell
.\compara_literal.bat test_data/ordenado_100.txt test_data/randomico_1000.txt test_data/decrescente_10000.txt
```

Isso gera `resultado.html` por padrao.

Exemplo com 3 arquivos:

```powershell
dotnet run -- test_data/randomico_1000.txt test_data/ordenado_1000.txt test_data/decrescente_1000.txt -o resultado.html
```

Modo do BubbleSort:

- Padrao: `optimized` (contagem eficiente por inversoes)
- Opcional: `literal` (execucao passo a passo do BubbleSort)

Exemplo com modo literal:

```powershell
dotnet run -- test_data/ordenado_100.txt test_data/randomico_100.txt test_data/decrescente_100.txt -o resultado.html --bubble-mode literal
```

Exemplo com todos os arquivos da pasta:

```powershell
dotnet run -- test_data/*.txt -o resultado.html
```

## Saida

O programa gera um arquivo HTML contendo a tabela no formato solicitado:

- Inicio: <html><head><title>PAA - Trabalho 2</title></head><body><table border=1>
- Cabecalho: Arquivo, BubbleSort, QuickSort, BucketSort
- Uma linha por arquivo de entrada com os numeros de operacoes
- Final: </table></body></html>
