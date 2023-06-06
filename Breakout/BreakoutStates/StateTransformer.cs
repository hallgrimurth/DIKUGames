using System;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;
using Breakout.BreakoutStates;
/// <summary>
/// A helper class for transforming between GameStateType enum and string representations.
/// </summary>
public class StateTransformer {
    /// <summary>
    /// Transforms a string representation of a state to a GameStateType enum.
    /// </summary>
    /// <param name="state">The string representation of the state.</param>
    /// <returns>The corresponding GameStateType enum value.</returns>
    public static GameStateType TransformStringToState(string state) {
        switch (state) {
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
                throw new ArgumentException("Invalid state string.");
        }
    }

    /// <summary>
    /// Transforms a GameStateType enum to its string representation.
    /// </summary>
    /// <param name="state">The GameStateType enum value.</param>
    /// <returns>The string representation of the state.</returns>
    public static string TransformStateToString(GameStateType state) {
        switch (state) {
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
                throw new ArgumentException("Invalid GameStateType.");
        }
    }
}