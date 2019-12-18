using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using LondonTowerLibrary.Properties;
using LondonTowerLibrary.ViewModels;
using Newtonsoft.Json;


namespace LondonTowerLibrary
{/// <summary>
/// Pattern Factory, Classe servant à créer completement un test LondonTower avec chaque niveaux configuré.
/// </summary>
    public class FactoryLondonTower
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="towerVM"></param>
        /// <returns></returns>
        public static LondonTower GetTowerOfLondon(LondonTowerVM towerVM)
        {
            LondonTower ToL = LoadTowerOfLondon<LondonTower>(towerVM.NbPegs);
            ToL.DateAndTime = towerVM.DateAndTime;
            ToL.Personn = towerVM.Personne;
            ToL.VisualHelp = towerVM.VisualHelp;

            return ToL;
        }

        /// <summary>
        /// Déserialisation du fichier de configuration de LondonTower suivant le niveau demander
        /// </summary>
        /// <typeparam name="T">Type Générique</typeparam>
        /// <param name="level">Entier représentant le niveau de difficulté choisi (3,4 ou 5 Peg)</param>
        /// <returns>Retourne l'object deserialiser et construit avec les valeurs contenue dans le fichier de config</returns>
        private static T LoadTowerOfLondon<T>(int level)
        {
           string path = "./Config/TowerOfLondon_Lvl" + level + ".txt";
           var str = File.ReadAllText(path);
            T Tempo = JsonConvert.DeserializeObject<T>(str);
            return Tempo;
        }
               
    }
}
