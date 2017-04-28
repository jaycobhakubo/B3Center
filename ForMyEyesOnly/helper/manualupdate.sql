select * from b3.dbo.B3_Session 
select * from B3SettingsGlobal where SettingID = 41

update b3.dbo.B3_Session 
set enforcemix = 'F'
update B3SettingsGlobal
set SettingValue ='F'
where SettingID = 41

select * from b3.dbo.B3_SystemConfig
select * from B3SettingsGlobal where SettingID = 53

update b3.dbo.B3_SystemConfig
set CommonRNGBallCall = 'T'
update B3SettingsGlobal
set SettingValue ='T'
where SettingID = 53


select Value from B3SettingGame where SettingID = 58

select * from B3MathPackageDef where MathPackageID in (select Value from B3SettingGame where SettingID = 58)

select * from B3MathPackageDef  where GameID  = 1