using DigiBank.Classes;

namespace DigiBank.Contratos
{
    public interface IConta
    {
         void Deposita (double valor);
         bool Saca(double valor);
        double ConsultaSaldo();
        string GetCodigoBanco();
        string GetNumeroAgencia();
        string GetNumeroConta();
        List<Extrato> Extrato();
    }
}