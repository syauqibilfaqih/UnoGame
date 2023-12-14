# UnoGame
A simple backend program for UNO based on C#

## Alogrithm
1. **Initialization:**
- Get players names. Store it in a `List<IPlayer>`
- Shuffle the UNO deck using `ShuffleDeck()`. Where to store the data tho?
- Deal seven cards to each player using `void AddCardToPlayer(IPlayer player, params Card[] cards)`.
- Place the remaining deck face down to form the draw pile.
- Flip the top card from the draw pile to create the discard pile.

2. **Game Loop**
- Players take turns in a clockwise order.
- On a player's turn:
  - Check if they have a playable card (matching color or number).
  - If they have a playable card, they can choose to play it `bool IsAnyCardPossible (IPlayer)`.
  - If not, they must draw a card from the draw pile `void AddCardToPlayer(IPlayer player, params Card[] cards)`.
  - Check if the drawn card can be played `bool IsAnyCardPossible (IPlayer)`. If yes, play it; otherwise, end the turn `IPlayer _playerNow`.
  - If a player has one card left, they must say "UNO." `PlayerSayUno (IPlayer) bool`

3. **Special Cards**
- Handle special cards (Skip, Reverse `void SwitchGameDirection(Direction)`, Draw Two, Wild, Wild Draw Four) according to their rules.
- Allow players to choose the next color for Wild cards. `PlayerPickColor(IPlayer, Color)`

4. **Winning**
- A player wins when they play their last card. `bool IsGameOver (IPlayer)` `GetWinner() IPlayer`

5. **Scoring**
- Calculate scores based on the remaining cards in opponents' hands. `int GetPlayerScore(IPlayer)` `GetAllScore() Dictionary~IPlayer.int~` 
