using System;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;
using Breakout.BreakoutStates;
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
                case "GAME_WON":
                    return GameStateType.GameWon;
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
                case GameStateType.GameWon:
                    return "GAME_WON";
                default:
                    throw(new ArgumentException());
            }
        }
    } 