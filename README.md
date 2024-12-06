# Description
Simple project for automating the conversion between Switch Taiko Assets & Steam ones.<br>
Tested with Taiko no Tatsujin: Rhythm Festival and the Shin Ultramix PLUS ULTRA v3 for the Switch.

# Requirements
- tns2tool (https://github.com/cainan-c/tns2tool) - Tested with 0.2.2
- DonderfulJSONExtractor (https://github.com/despairoharmony/DonderfulTools)

# Settings
- StartingUniqueID: First ID to use for new songs (Inclusive)
- MaxUniqueID: Highest ID to use for new songs (Exclusive)
- ExcludedSongs: Comma-separated list of IDs to exclude from processing

NOTE: The script checks if the ID is already in use so nothing is overwritten.

# Usage
1. Move dependencies into their respective folders
2. Move required files into the IN folder
   1. IN\steam: initial_possession.bin + musicinfo.bin
   2. IN\switch: musicdata.unity3d + worddata.unity3d
   3. IN\switch\csv: Song CSVs
   4. IN\switch\fumen: Song Fumen files (Subdirectories are allowed)
   5. IN\switch\presong: Presong files
   6. IN\switch\song: Main Song files
3. Run the script
   1. -q flag can be passed to mute some of the Output
4. Copy files from the OUT folder to the respective Steam folder