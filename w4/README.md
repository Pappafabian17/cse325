# W04 Assignment: Build Connect Four Game with Blazor

Blazor web application developed for Week 04 of CSE 325, based on the Microsoft Learn module **"Build Connect Four game with Blazor"**.

## Overview of Implemented Features

- **Blazor Setup & Interactivity**: Configured a Blazor Web App in .NET 8 using `InteractiveServer` render mode per component.
- **Board Component & Styles**: Created `Board.razor` and `Board.razor.css` to render the 7x6 board grid, piece drop animations, and column placement buttons.
- **Game State Logic**: Implemented `GameState.cs` as a singleton service in `Program.cs` to manage turns, board state, win checking across horizontal/vertical/diagonal directions, and game resets.
- **Color Selection**: Added `BoardColor`, `Player1Color`, and `Player2Color` parameters using `System.Drawing.Color`, with live color inputs and preset buttons (Classic, Green, Sky) to customize pieces and board colors in real time.
- **Scoreboard & Win Streak Tracking**: Added a scoreboard card tracking total wins for Player 1, Player 2, and ties, along with consecutive win streak detection.

## Running the Application

From `w4/ConnectFour`:
```bash
dotnet run --launch-profile http
```
Access the application at `http://localhost:5155`.
