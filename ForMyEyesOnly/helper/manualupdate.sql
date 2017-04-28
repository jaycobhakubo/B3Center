select * from b3.dbo.B3_Session 
select * from B3SettingsGlobal where SettingID = 41

update b3.dbo.B3_Session 
set enforcemix = 'T'
update B3SettingsGlobal
set SettingValue ='T'
where SettingID = 41

select * from b3.dbo.B3_SystemConfig
select * from B3SettingsGlobal where SettingID = 53

update b3.dbo.B3_SystemConfig
set CommonRNGBallCall = 'T'
update B3SettingsGlobal
set SettingValue ='T'
where SettingID = 53