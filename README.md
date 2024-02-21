
# Unity Save Data Serialization 

A compact ByteFormatter-based Save & Load System for Game Profiles that I created for Small & Portfolio Games

## Features

- Compact & Slim
- Expandable: Allows for Custom Serialization Integration(Classes etc.)
- Multiple Profile Support
- Async Profile Saving with Task States(Can be used for displaying a "Saving" Icon ingame e.g.)


## Usage

To use the SaveSystem, you need a Singleton Instance/GameObject of the SavesManager class! This should be added to your active Scene Layout etc. as early as the Main Menu, so players can (optionally) choose their Profile or the single Profile can be loaded!

## Examples 
The Repo contains integrated usage Examples!


## Known Issues
Doesn't have backwards compatibility -> If you add more Serializables etc. it will no longer load the Save Data properly.
