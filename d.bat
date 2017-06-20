rd /s /q "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\About"
REM rd /s /q "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\Assemblies"
rd /s /q "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\Defs"
rd /s /q "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\Languages"
xcopy "About" "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\About" /e /d /i /y
REM xcopy "Assemblies" "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\Assemblies" /e /d /i /y
xcopy "Defs" "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\Defs" /e /d /i /y
xcopy "Languages" "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\Languages" /e /d /i /y
copy "LICENSE" "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\LICENSE" /y
copy "README.md" "D:\Game\Steam\steamapps\common\RimWorld\Mods\RimWorld PawnName Chinese\README.md" /y