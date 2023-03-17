using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
//using System.IO;
//using DIKUArcade.Math;
//using DIKUArcade.GUI;
//using DIKUArcade.Events;
//using DIKUArcade.Input;



namespace Galaga.Squadron {
public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    int MaxEnemies {get;}
    void CreateEnemies (List<Image> enemyStride,
        List<Image> alternativeEnemyStride);
    }
}