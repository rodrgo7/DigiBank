using System.Globalization;

namespace DigiBank.Classes
{
    public static class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int opcao = 0;
        public static void WriteLine(this string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void TelaPrincipal ()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 1 - Cria Conta");
            Console.WriteLine();
            Console.WriteLine(" 2 - Entrar com CPF");
            Console.WriteLine();
            Console.WriteLine(" 3 - Fechar Sistema");

            Console.WriteLine();
            Console.Write(">  ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                TelaCriarConta();
                    break;
                case 2:
                TelaLogin();
                    break;
                case 3:
                Console.Clear();
                Console.WriteLine(" Pressione qualquer botão para encerrar.");
                Console.ReadKey(true);    
                    
                Console.Clear();

                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("       Opção Inválida!      ");
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    TelaPrincipal();
                    break;            
            }
            Console.Clear();
       }  
        private static void TelaCriarConta()
       {
            Console.Clear();

            Console.WriteLine(" DigiBank");
            Console.WriteLine();
            Console.WriteLine(" Criar Conta");
            Console.WriteLine();
            Console.Write(" Nome: ");
            string nome = Console.ReadLine();
            Console.Write(" CPF: ");
            string cpf = Console.ReadLine();
            Console.Write(" Senha: ");
            string senha = Console.ReadLine();
            Console.WriteLine();

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Conta criada com Sucesso!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            //Esse codigo faz com que o sistema espere os segundos, setados no codigo para executar a proxima ação
            // Exemplo (2500segundos)
            Thread.Sleep(2500);

            TelaLogada(pessoa);
       }
        private static void TelaLogin()
       {
            Console.Clear();

            Console.WriteLine(" Login");
            Console.WriteLine();
            Console.Write(" CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("====================");
            Console.Write(" Senha: ");
            string senha = Console.ReadLine();            
            Console.WriteLine("====================");

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);

            if(pessoa != null)
            {
                //Tela de Boas Vindas
                //Tela Logada
                TelaBoasVindas(pessoa);
                TelaLogada(pessoa);
                

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Login, feito com sucesso!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            } else 
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Usuário não cadastrada!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                Thread.Sleep(2500);

                TelaPrincipal();
            }
       }
        private static void TelaBoasVindas(Pessoa pessoa)
       {
            Console.Clear();

            string msgTelaBemVindo =
            $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoBanco()} | Agência: {pessoa.Conta.GetNumeroAgencia()} | Número da Conta: {pessoa.Conta.GetNumeroConta()}";

            Console.WriteLine("  DigiBank");
            Console.WriteLine();
            Console.WriteLine($" Seja bem-vindo, {msgTelaBemVindo}");
            Console.WriteLine("");
       }
        private static void realizarDeposito(Pessoa pessoa) 
        {
            Console.Clear();

            Console.WriteLine(" DigiBank - Deposito");
            Console.WriteLine();
            Console.Write(" Saldo em conta: R$" + pessoa.Conta.ConsultaSaldo().ToString("F2", CultureInfo.InvariantCulture));
            Console.Write(" | Valor à depositar: R$");

            double valor = double.Parse(Console.ReadLine());

            pessoa.Conta.Deposita(valor);

            Console.Write("     Deseja confirmar o deposito de: R$" + valor.ToString("F2", CultureInfo.InvariantCulture));

            Console.WriteLine(" (1- Sim | 2- Não)");            

            Console.WriteLine();
            Console.Write(">  ");
            
            int opcao = int.Parse(Console.ReadLine());

            if(opcao == 1)
            {
                "Deposito efetuado com Sucesso!".WriteLine(ConsoleColor.Green);
                //WriteLine("    Deposito efetuado", ConsoleColor.Green);
            }
            else if (opcao == 2)
            {
                voltarLogado(pessoa);
            }
            else 
            {
                Console.WriteLine(" Opção Inválida");
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLine(" Deposito efetuado", ConsoleColor.Red);
            }
            Thread.Sleep(2500);
            TelaLogada(pessoa);
        }
        private static void realizarSaque(Pessoa pessoa)
        {
            Console.Clear();

            Console.WriteLine(" DigiBank - Saque  ");
            Console.WriteLine();
            Console.WriteLine(" Saldo atual: R$" + pessoa.Conta.ConsultaSaldo().ToString("F2",CultureInfo.InvariantCulture));

            Console.Write(" Qual o valor de saque R$");

                double valor = double.Parse(Console.ReadLine());

                pessoa.Conta.Saca(valor);            

                Console.WriteLine();
                Console.Write(">  ");

            Console.Write(" Deseja confirmar o saque de R$"+ valor.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine(" (1- Sim/ 2-Não)");           

            Console.WriteLine();
            Console.Write(">  ");
            
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                if (pessoa.Conta.Saca(valor) == true)
                //(pessoa.Conta.Saca(valor) == pessoa.Conta.ConsultaSaldo()))
                //(pessoa.Conta.Saca(valor) == pessoa.Conta.ConsultaSaldo(valor)))
                {
                    " Saque efetuado com Sucesso!".WriteLine(ConsoleColor.Green);
                }
                else {
                    " Saldo insuficiente para Saque".WriteLine(ConsoleColor.Red);
                }
                break;
            case 2:
                    "Saque não foi efetuado".WriteLine(ConsoleColor.White);
                    Console.WriteLine(" Voltando ao menu principal");
                    Thread.Sleep(2500);
                    TelaLogada(pessoa);
                break;
            default:
                Console.WriteLine(" Opção inválida");

                    Thread.Sleep(2500);
                    realizarSaque(pessoa);
                break;
            }
            Thread.Sleep(2500);
            Console.Clear();
            TelaLogada(pessoa);           
        }
        private static void consultarSaldo(Pessoa pessoa)
        {
            Console.Clear();

            Console.WriteLine(" DigiBank - Saldo");
            Console.WriteLine();
            Console.WriteLine(" Saldo: R$" + pessoa.Conta.ConsultaSaldo().ToString("F2"), CultureInfo.InvariantCulture);
            Console.WriteLine("====================");
            Console.WriteLine(" 1 - Sair   ");
            Console.WriteLine();
            Console.Write(">   ");
            opcao = int.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 1:
                // Realizar Deposito
                TelaLogada(pessoa);
                    break;
                default:
                    consultarSaldo(pessoa);
                break;  
            }         
        }
        private static void verExtrato(Pessoa pessoa)
        {
            Console.Clear();

            Console.WriteLine(" DigiBank - Extrato");
            Console.WriteLine();

            if(pessoa.Conta.Extrato().Any())
            {
                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach(Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.WriteLine();
                    Console.WriteLine($" Data: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}");
                    Console.WriteLine($" Tipo de Movimentação: {extrato.Descricao}  ");
                    Console.WriteLine($" Valor: {extrato.Valor}  ");
                    Console.WriteLine("====================");
                }

                Console.WriteLine();
                Console.WriteLine($" SUB TOTAL:  {total}   ");
                Console.WriteLine("====================");
            }
            else 
            {
                Console.WriteLine(" Não há extrato há ser exibido! ");
            }
            voltarLogado(pessoa);
        }
        private static void TelaLogada(Pessoa pessoa)
        {
            Console.Clear();
            
            Console.WriteLine();
            Console.WriteLine(" DigiBank");
            TelaBoasVindas(pessoa);
            Console.WriteLine();
            Console.WriteLine(" 1 - Realizar Deposito");
            Console.WriteLine("====================");
            Console.WriteLine(" 2 - Realizar Saque");
            Console.WriteLine("====================");
            Console.WriteLine(" 3 - Consultar Saldo");
            Console.WriteLine("====================");
            Console.WriteLine(" 4 - Extrato");
            Console.WriteLine("====================");
            Console.WriteLine(" 5 - Sair");
            Console.WriteLine("====================");
            
            Console.WriteLine();
            Console.Write(">   ");

            opcao = int.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 1:
                // Realizar Deposito
                realizarDeposito(pessoa);
                    break;
                case 2:
                // Realizar Saque
                realizarSaque(pessoa);
                    break;
                case 3:
                // Consultar Saldo
                consultarSaldo(pessoa);
                    break;
                case 4:
                // Extrato
                verExtrato(pessoa);
                    break;
                 case 5:
                 // Sair
                 TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
        private static void voltarLogado(Pessoa pessoa)
        {
            Console.Clear();

            Console.WriteLine(" DigiBank");
            
            Console.WriteLine();
            Console.WriteLine(" 1 - Voltar");
            Console.WriteLine("====================");
            Console.WriteLine(" 2 - Sair");
            Console.WriteLine("====================");

            opcao = int.Parse(Console.ReadLine());

            if(opcao == 1)
                TelaLogada(pessoa);
            else if (opcao == 2) 
                TelaPrincipal();
            else 
                Console.WriteLine(" Opção inválida!");
                Console.Clear();
            return;
        }
        private static void voltarDeslogado()
        {
            Console.Clear();

            Console.WriteLine(" DigiBank");
            Console.WriteLine();
            Console.WriteLine(" 1 - Voltar");
            Console.WriteLine("====================");
            Console.WriteLine(" 2 - Sair");
            Console.WriteLine("====================");

            opcao = int.Parse(Console.ReadLine());

            if(opcao == 1)
                TelaPrincipal();
            else 
                Console.WriteLine(" Opção inválida!");
                Console.WriteLine("====================");
                Console.Clear();
            return;
        }
    }
}

