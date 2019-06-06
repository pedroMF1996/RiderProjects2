using System;
using System.Security.Cryptography;

namespace RSA.API.Controllers
{
    public class TextoController
    {
        //Criar dois métodos 
            //Criptografar
                /* Salva o texto inicial no banco,
                 * Criptografa,
                 * Salva o criptografado no banco
                 * devolve a resposta do txt criptografado
                 */
            
            //Descriptografar
                /* Salva o texto inicial no banco,
                 * Descriptografa,
                 * Salva o descriptografado no banco
                 * devolve a resposta do txt descriptografado
                 */
                
                /* Outra forma de descriptografar é fazer uma pesquisa linq
                 * no banco de dados no id estrangeiro e pegar o texto original  
                 */
                
                /* Outra forma de descriptografar é receber o texto criptografado
                 * fazer uma pesquisa linq pelo correspondente do texto criptografado
                 * no banco de dados e pega o texto original  
                 */
    }
}