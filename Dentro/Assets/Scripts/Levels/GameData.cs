using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
   public int levelnum;
   public Stats currentstats;
   public Stats standardstats;
   public static List<string> leftLevelNames1;
   public string currentLevel;
   public void setLeftNames(List<string> left){
    leftLevelNames1 = left;
   }
   public List<string> getLeftNames(){
      return leftLevelNames1;
   }
   public void setcurrentstats(Stats a){
    currentstats = a;
   }
   public void setstandardstats(Stats a){
    standardstats = a;
   }
}
