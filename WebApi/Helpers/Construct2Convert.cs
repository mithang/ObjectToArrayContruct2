using Newtonsoft.Json;

namespace ObjectToArrayContruct2.Helpers;

public static class Construct2Convert
{
    /// <summary>
    /// Prove métodos para converter objetos em formatos que possam ser reconhecidos pela game engine Construct 2 da Scirra 
    /// </summary>

        #region Atributos

        #endregion

        #region Métodos

        /// <summary>
        /// Recebe um objeto e converte ele para o formato do Dictionary do Construct 2
        /// </summary>
        /// <typeparam name="T">Classe do objeto a serem convertido</typeparam>
        /// <param name="myObject">Objeto a ser convertido para o formato do Dictionary do Construct</param>
        /// <returns>object formatado para o Construct</returns>
        public static object ToDictionary<T>(T myObject)
        {
            // Cria um object com a estrutura básica de um Dictionary do Construct e também incluí o objeto nele
            object dictionaryDoConstruct = new { c2dictionary = true, data = myObject }; // Inclui os dados no dictionaryDoConstruct

            return dictionaryDoConstruct; // Retorna o dictionaryDoConstruct no formato de Dictionary do Construct já convertido para JSON
        }

        /// <summary>
        /// Recebe um conjunto de objetos e os converte para o formato de Array do Construct 2 
        /// </summary>
        /// <typeparam name="T">Classe dos objetos a serem convertidos</typeparam>
        /// <param name="MyObjects">Conjunto de objetos a serem convertidos para o formato do Array do Construct</param>
        /// <returns>object formatado para o Construct</returns>
        public static object ToArray<T>(IEnumerable<T> MyObjects)
        {
            int tamanhoASerDefinido = MyObjects.Count();
            const int tamanhoMinimo = 1; // Tamanho mínimo de um índice de um Array no Construct

            // Obs.: O menor tamanho possível de cada índice (X, Y e Z) é 1, esse é um padrão do Construct

            // Corrige o tamanhoASerDefinido, caso seja necessário
            if (tamanhoASerDefinido < tamanhoMinimo) // Se o tamanhoASerDefinido for 0...
            {
                tamanhoASerDefinido = tamanhoMinimo; // Corrige para o tamanhoMinimo
            }

            // Cria um vetor para representar qual será o tamanho do array que irá ser enviado para o Construct
            int[] size = { tamanhoASerDefinido, tamanhoMinimo, tamanhoMinimo }; // Tamanho em X, Y, Z

            // Obs.: O Construct nomeia os índices de seus Arrays como X, Y e Z (na verdade é uma matriz)

            // Cria um array (matriz) para inserir os objetos
            object[,,] arrayDeDictionaries = new object[tamanhoASerDefinido, tamanhoMinimo, tamanhoMinimo];
            arrayDeDictionaries[0, 0, 0] = 0; // Define o valor do índice [0, 0, 0] como sendo 0, esse é um padrão do Costruct

            // Preenche o arrayDeDictionaries com os objetos, que será enviado para o Construct
            for (int i = 0; i < MyObjects.Count(); i++)
            {
                object dictionaryDoConstruct = Construct2Convert.ToDictionary(MyObjects.ElementAt(i)); // Insere o objeto dentro de um dictionaryDoConstruct
                string dictionaryConvertidoParaJSON = JsonConvert.SerializeObject(dictionaryDoConstruct); // Converte o dictionaryDoConstruct para JSON
                arrayDeDictionaries[i, 0, 0] = dictionaryConvertidoParaJSON; // Adiciona o objeto já convertido para Dictionary e para JSON no array
            }

            // Cria um object com a estrutura básica de um Array do Construct e também incluí o tamanho e os objetos nele
            object arrayDoConstruct = new { c2array = true, size = size, data = arrayDeDictionaries }; // Inclui o tamanhoDoArray e o arrayDeDictionaries com os objetos já convertidos para JSON

            return arrayDoConstruct; // Retorna o arrayDoConstruct no formato de Array do Construct
        }

        /// <summary>
        /// Recebe um conjunto de objetos e os converte para o formato de Array do Construct 2
        /// </summary>
        /// <typeparam name="T">Classe dos objetos a serem convertidos</typeparam>
        /// <param name="objetos">Conjunto de objetos a serem convertidos para o formato do Array do Construct</param>
        /// <returns>object formatado para o Construct</returns>
        public static object ToArray<T>(IQueryable<T> objetos)
        {
            // Chama o método "ConverterParaArray" enviando uma lista como parâmetro
            return Construct2Convert.ToDictionary(objetos.ToList()); // Retorna o arrayDoConstruct no formato de Array do Construct
        }

        #endregion


}