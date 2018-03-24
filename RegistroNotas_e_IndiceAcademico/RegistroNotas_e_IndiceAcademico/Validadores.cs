using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroNotas_e_IndiceAcademico
{
    class Validadores
    {
        //validador numerico
        public static bool ValidadorNumerico(char e)
        {
            string caracteresPermitidos = "0123456789\b"; //\b es igual a retroceder
            bool existe;

            existe = caracteresPermitidos.Contains(e);//contains devuelve true si el caracter esta dentro de la cadena

            if (existe == true)
            {
                return false; //permite escribir el caracter
            }
            else
            {
                return true; //evita escribir el caracter
            }
        }

        public static bool ValidadorMatricula(char e)
        {
            string caracteresPermitidos = "0123456789-\b"; //\b es igual a retroceder
            bool existe;

            existe = caracteresPermitidos.Contains(e);//contains devuelve true si el caracter esta dentro de la cadena

            if (existe == true)
            {
                return false; //permite escribir el caracter
            }
            else
            {
                return true; //evita escribir el caracter
            }
        }

        public static bool ValidadorFlotante(char e)
        {
            string caracteresPermitidos = "0123456789.\b"; //\b es igual a retroceder
            bool existe;

            existe = caracteresPermitidos.Contains(e);//contains devuelve true si el caracter esta dentro de la cadena

            if (existe == true)
            {
                return false; //permite escribir el caracter
            }
            else
            {
                return true; //evita escribir el caracter
            }
        }
    }
}
