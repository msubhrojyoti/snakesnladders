# Snakes and Ladders

Demonstration of `simulation`, `statistics` and `testability` of the game.

### Interfaces

- IAdvancer
- IBoard
- ICharacter
- IGame
- IGameStats
- IPlayer
  
### Models (Business Domain)

- BasicSnakesAndLadders
- Board
- Character
- Dice
- GameStats
- Player
- Ladder
- Snake
- Statisics
- Snake
- Trap (An extension to the game)

### Variation

- SnakesAndLaddersWithTraps

### Factory

- AdvancerFactory
- PlayerFactory
- CharacterFactory
- BoardFactory
- StatsFactory
- GameFactory

### Custom Exceptions

- InvalidCharacterPositionException

### Logging
- SeriLog
- Console and File

### Tests
- Snakes and Ladder single player tests
- Snakes and Ladder multiple player tests
- Snakes and Ladder simulations with multiple players and multiple games
- Snakes and Ladder stats validation test