select * from b3.dbo.B3_Session 
select * from daily.dbo.B3SettingsGlobal where SettingID = 41

select * from b3.dbo.B3_SystemConfig
select * from daily.dbo.B3SettingsGlobal where SettingID = 53

update b3.dbo.B3_Session 
set enforcemix = 'F'
update B3SettingsGlobal
set SettingValue ='F'
where SettingID = 41

update b3.dbo.B3_SystemConfig
set CommonRNGBallCall = 'T'
update B3SettingsGlobal
set SettingValue ='T'
where SettingID = 53

--select Value from B3SettingGame where SettingID = 58
select * from daily.dbo.B3MathPackageDef where MathPackageID in (select Value from daily.dbo.B3SettingGame where SettingID = 58)
--select * from B3MathPackageDef  where GameID  = 1

select * from dbo.CrazyBout_GameSettings
select * from dbo.JailBreak_GameSettings
select * from  dbo.MayaMoney_GameSettings
select * from  dbo.Spirit76_GameSettings
select * from dbo.TimeBomb_GameSettings
select * from  dbo.UKickEm_GameSettings
select * from dbo.WildBall_GameSettings
select * from  dbo.WildFire_GameSettings

select  * from Daily.dbo.B3SettingGame where SettingID = 58

select * from Daily.dbo.B3SettingGame where B3GameID = 4

