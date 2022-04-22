Este projecto ignora a parte do front-end por não ser o foco do exercício.

Devido a ser um exercício que necessitava de utilizadores tomei a liberdade de fazer uma tabela simples de utilizadores. Fiz também um login simples, não adicionei authentification porque, novamente, não era parte essencial do exercício. 

A ideia de ter uma tabela de utilizadores era essencial para o exercício, é preciso saber o user ID para registar o voto de um determinado utilizador num determinado post. Para esta situação criei uma tabela que liga ambas com 2 chaves estrangeiras. Alternativamente também podia guardar os IDs dos utilizadores que votaram "up" ou "down" numa lista na tabela do Post. 

A actualização dos dados das tabelas é feita neste código, alternativamente deveria ser feito numa store procedure ou com triggers para evitar problemas na app no futuro.

O Id do user é passado como parâmetro e guardado numa variável global dentro do controller "Posts", isto não é ideal de todo, eu tentei guardar o ID com Session só para a funcionalidade do exercício mas não consegui uma maneira que funcionasse, de forma a prosseguir com o verdadeiro scope do exercício ignorei este pormenor. Assumi que na aplicação em questão fosse possível recolher o ID do User de uma forma correcta e a variável _userId serve apenas para efeitos dessa "simulação".

No ficheiro Pages/Shared/Index.cshtml está escrito em comentário código para popular a DB. 
