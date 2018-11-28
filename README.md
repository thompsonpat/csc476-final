# CSC-476 Final Game
Top-down pirate game.

# Why the game is large and complex.
This game is large and complex because it contains many game systems found in most successful games. This game will contain most of the topics we have covered in class. Some of these topics include, but are not limited to: sprites, animations, colisions and code-generated enemies. This game will also have a much more fleshed out progression system than any we have made in the class so far. This is important because it is what bring players back time after time. To provide a solid progression system to players, all systems that contribute to said progression system must be well balanced to incentivize the player with a reason to continue playing the game. This game will also attempt to provide a basic AI for enemies in the game. This is something that has not been attempted in the class and will require an understanding on how the enemy will interact with aspects of the map and the game mechanics.

# Game Design Document

## Concept
The purpose of the game is to provide the player with a fun and easily approachable pirate boat game with solid progression system to keep the player wanting to conquer the high seas.

## Game Structure
The player randomly spawns in the ocean with randomly generated (and still generating) enemy ships. The player sinks the other ships to gather resources and upgrade their ship to take on more menacing enemies.

## Objective
To have the largest, most powerful ship on the high seas.

## Graphics
The game will be top-down centered around the player's ship.

## Map
The player will spawn in a vast ocean with defined map borders preventing the player from leaving the map. The map will also contain small islands that cannot be traveled through.

## Enemies
### Enemy Ships
The enemy ships will spawn with a random level and sail around until another ship comes within it agro. radius. The ships' levels will determine the speed, health, and amount of cannons a ship has.

### Enemy Drops
Once an enemy ship has been sunk, the ship will drop men, wood and gold.

## Progression

### Death
If the player dies, they lose everything and restart.

### Resources
There will be 3 primary resources.
1. Gold
2. Men
3. Wood

#### Gold
Gold is used to upgrade the quality of cannons and sails. It is also used to hire additional men to work on the ship.

#### Men
Men are used to man the cannons and sails.

#### Wood
Wood is used to upgrade the size of the ship, build new sails and repair the ship when it has taken damage.

### Ship Size
Ship size can be upgraded using wood. Once the ship is upgraded, it increases the ship's health and provides additional slots for men, cannons and sails.

### Sails
Sails increase the max acceleration and velocity of the ship.

### Cannons
Additional cannons increase the firepower and projeciles per volley from a ship.

### Health
As the ship grows larger, the health increases. The larger the ship, the harder it is to maneuver (turn) the ship.

## Bonus Features (if time allows)
### Ports
Ports will spawn on islands and will have an import and export associate with the port. These resources will allow for trading from port to port to gain gold. This new resource system would also allow for addtional feature of cargo ships. Cargo ships move from port to port transporting goods. These ships can be attacked to take their resouces, but as a tradeoff, a bounty is applied to the ship which will cause other ships to seek out and destroy the player's ship. Additionally, these ports will generate men that can be hired for money.

### Ship Classes
Ships will be able to choose a specialization which will provide specific advantages and disadvantages depending on the class.
1. Cargo Ship - Increases the amount of cargo the player's ship can carry. Lower fire power potential.
2. Warship - Increases the maximum amount of cannons. (Slower, carry less)
3. Ram - Allows the player's ship to ram other ships to deal damage.
4. Row Boats - Allows the production of oars to increase ship mobility (turning rate).
