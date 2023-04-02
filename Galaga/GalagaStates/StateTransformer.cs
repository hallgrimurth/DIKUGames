using System;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;
using Galaga.GalagaStates;
public class StateTransformer{

        public static GameStateType TransformStringToState(string state){
            switch(state){
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "GAME_OVER":
                    return GameStateType.GameOver;
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                default:
                    throw(new ArgumentException());
            }
        }

        public static string TransformStateToString(GameStateType state){
            switch(state){
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.GameOver:
                    return "GAME_OVER";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                default:
                    throw(new ArgumentException());
            }
        }
    } 