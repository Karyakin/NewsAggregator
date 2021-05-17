USE NewsAggregator
go



  SELECT 
			 Users.Id as UserId
			,Users.[Login]
			,ContactDetails.Id as KontakDetailsID
			,Countries.[Name], [�ities].[Name] as City
			,EMails.UserEMail as EMail
			,Phones.PhoneNumber as Phone
  FROM Users
			JOIN ContactDetails on ContactDetails.Id = Users.ContactDetailsId
			JOIN Countries on Countries.Id = ContactDetails.CountryId			
			JOIN [�ities] on [�ities].Id = ContactDetails.CityId
			JOIN EMails on EMails.ContactDetailsId = ContactDetails.Id 
			JOIN Phones on Phones.ContactDetailsId = ContactDetails.Id

go


Select Users.[Login], Roles.Name as [Role], Countries.Name, [�ities].[Name], EMails.UserEMail, Phones.PhoneNumber, Users.DayOfBirth
from Users
			JOIN Roles on Users.RoleId = Roles.Id
			JOIN ContactDetails on ContactDetails.Id = Users.ContactDetailsId
			JOIN Countries on Countries.Id = ContactDetails.CountryId			
			JOIN [�ities] on [�ities].Id = ContactDetails.CityId
			JOIN EMails on EMails.ContactDetailsId = ContactDetails.Id 
			JOIN Phones on Phones.ContactDetailsId = ContactDetails.Id

			
--INSERT INTO Countries (Id, CountryCod,[Name])

values				
(NEWID(),895,			N'�������'),
(NEWID(),036,			N'���������'),
(NEWID(), 040,			N'�������'),
(NEWID(), 031,			N'�����������'),
(NEWID(), 008,			N'�������'),
(NEWID(), 012,			N'�����'),
(NEWID(), 016,			N'������������ �����'),
(NEWID(), 660,			N'�������'),
(NEWID(), 024,			N'������'),
(NEWID(), 020,			N'�������'),
(NEWID(), 010,			N'����������'),
(NEWID(), 028,			N'������� � �������'),
(NEWID(), 032,			N'���������'),
(NEWID(), 051,			N'�������'),
(NEWID(), 533,			N'�����'),
(NEWID(), 004,			N'����������'),
(NEWID(), 044,			N'������'),
(NEWID(), 050,			N'���������'),
(NEWID(), 052,			N'��������'),
(NEWID(), 048,			N'�������'),
(NEWID(), 112,			N'��������'),
(NEWID(), 084,			N'�����'),
(NEWID(), 056,			N'�������'),
(NEWID(), 204,			N'�����'),
(NEWID(), 060,			N'�������'),
(NEWID(), 100,			N'��������'),
(NEWID(), 068,			N'�������'),
(NEWID(), 535,			N'������'),
(NEWID(), 070,			N'������ � �����������'),
(NEWID(), 072,			N'��������'),
(NEWID(), 076,			N'��������'),
(NEWID(), 086,			N'���������� ���������� � ��������� ������'),
(NEWID(), 096,			N'������-����������'),
(NEWID(), 854,			N'�������-����'),
(NEWID(), 108,			N'�������'),
(NEWID(), 064,			N'�����'),
(NEWID(), 548,			N'�������'),
(NEWID(), 348,			N'�������'),
(NEWID(), 862,			N'���������'),
(NEWID(), 092,			N'���������� �������(����������),'),
(NEWID(), 850,			N'���������� �������(���),'),
(NEWID(), 704,			N'�������'),
(NEWID(), 266,			N'�����'),
(NEWID(), 328,			N'������'),
(NEWID(), 332,			N'�����'),
(NEWID(), 270,			N'������'),
(NEWID(), 288,			N'����'),
(NEWID(), 312,			N'���������'),
(NEWID(), 320,			N'���������'),
(NEWID(), 324,			N'������'),
(NEWID(), 624,			N'������-�����'),
(NEWID(), 276,			N'��������'),
(NEWID(), 831,			N'������'),
(NEWID(), 292,			N'���������'),
(NEWID(), 340,			N'��������'),
(NEWID(), 344,			N'�������'),
(NEWID(), 308,			N'�������'),
(NEWID(), 304,			N'����������'),
(NEWID(), 300,			N'������'),
(NEWID(), 268,			N'������'),
(NEWID(), 316,			N'����'),
(NEWID(), 208,			N'�����'),
(NEWID(), 832,			N'������'),
(NEWID(), 262,			N'�������'),
(NEWID(), 212,			N'��������'),
(NEWID(), 214,			N'������������� ����������'),
(NEWID(), 818,			N'������'),
(NEWID(), 894,			N'������'),
(NEWID(), 732,			N'�������� ������'),
(NEWID(), 716,			N'��������'),
(NEWID(), 887,			N'�����'),
(NEWID(), 376,			N'�������'),
(NEWID(), 356,			N'�����'),
(NEWID(), 360,			N'���������'),
(NEWID(), 400,			N'��������'),
(NEWID(), 368,			N'����'),
(NEWID(), 364,			N'����'),
(NEWID(), 372,			N'��������'),
(NEWID(), 352,			N'��������'),
(NEWID(), 724,			N'�������'),
(NEWID(), 380,			N'������'),
(NEWID(), 132,			N'����-�����'),
(NEWID(), 398,			N'���������'),
(NEWID(), 116,			N'��������'),
(NEWID(), 120,			N'�������'),
(NEWID(), 124,			N'������'),
(NEWID(), 634,			N'�����'),
(NEWID(), 404,			N'�����'),
(NEWID(), 196,			N'����'),
(NEWID(), 417,			N'��������'),
(NEWID(), 296,			N'��������'),
(NEWID(), 156,			N'�����'),
(NEWID(), 166,			N'��������� �������'),
(NEWID(), 170,			N'��������'),
(NEWID(), 174,			N'������'),
(NEWID(), 178,			N'�����'),
(NEWID(), 180,			N'�����'),
(NEWID(), 408,			N'�����'),
(NEWID(), 410,			N'�����'),
(NEWID(), 188,			N'�����-����'),
(NEWID(), 384,			N'��� ������'),
(NEWID(), 192,			N'����'),
(NEWID(), 414,			N'������'),
(NEWID(), 531,			N'�������'),
(NEWID(), 418,			N'�������� �������-��������������� ����������'), 
(NEWID(), 428,			N'������'),
(NEWID(), 426,			N'������'),
(NEWID(), 430,			N'�������'),
(NEWID(), 422,			N'�����'),
(NEWID(), 434,			N'�����'),
(NEWID(), 440,			N'�����'),
(NEWID(), 438,			N'�����������'),
(NEWID(), 442,			N'����������'),
(NEWID(), 480,			N'��������'),
(NEWID(), 478,			N'����������'),
(NEWID(), 450,			N'����������'),
(NEWID(), 175,			N'�������'),
(NEWID(), 446,			N'�����'),
(NEWID(), 454,			N'������'),
(NEWID(), 458,			N'��������'),
(NEWID(), 466,			N'����'),
(NEWID(), 581,			N'����� ������������� ���������� ������� ����������� ������'), 
(NEWID(), 462,			N'��������'),
(NEWID(), 470,			N'������'),
(NEWID(), 504,			N'�������'),
(NEWID(), 474,			N'���������'),
(NEWID(), 584,			N'���������� �������'),
(NEWID(), 484,			N'�������'),
(NEWID(), 583,			N'����������'),
(NEWID(), 508,			N'��������'),
(NEWID(), 498,			N'�������'),
(NEWID(), 492,			N'������'),
(NEWID(), 496,			N'��������'),
(NEWID(), 500,			N'����������'),
(NEWID(), 104,			N'������'),
(NEWID(), 516,			N'�������'),
(NEWID(), 520,			N'�����'),
(NEWID(), 524,			N'�����'),
(NEWID(), 562,			N'�����'),
(NEWID(), 566,			N'�������'),
(NEWID(), 528,			N'����������'),
(NEWID(), 558,			N'���������'),
(NEWID(), 570,			N'����'),
(NEWID(), 554,			N'����� ��������'),
(NEWID(), 540,			N'����� ���������'),
(NEWID(), 578,			N'��������'),
(NEWID(), 784,			N'������������ �������� �������'),
(NEWID(), 512,			N'����'),
(NEWID(), 074,			N'������ ����'),
(NEWID(), 833,			N'������ ���'),
(NEWID(), 574,			N'������ �������'),
(NEWID(), 162,			N'������ ���������'),
(NEWID(), 334,			N'������ ���� � ������� ����������'),
(NEWID(), 136,			N'������� ������'),
(NEWID(), 184,			N'������� ����'),
(NEWID(), 796,			N'������� ����� � ������'),
(NEWID(), 586,			N'��������'),
(NEWID(), 585,			N'�����'),
(NEWID(), 275,			N'���������'),
(NEWID(), 591,			N'������'),
(NEWID(), 336,			N'������� ������� (����������� - ����� �������),'), 
(NEWID(), 598,			N'�����-����� ������'),
(NEWID(), 600,			N'��������'),
(NEWID(), 604,			N'����'),
(NEWID(), 612,			N'�������'),
(NEWID(), 616,			N'������'),
(NEWID(), 620,			N'����������'),
(NEWID(), 630,			N'������-����'),
(NEWID(), 807,			N'���������� ���������'),
(NEWID(), 638,			N'�������'),
(NEWID(), 643,			N'������'),
(NEWID(), 646,			N'������'),
(NEWID(), 642,			N'�������'),
(NEWID(), 882,			N'�����'),
(NEWID(), 674,			N'���-������'),
(NEWID(), 678,			N'���-���� � ��������'),
(NEWID(), 682,			N'���������� ������'),
(NEWID(), 748,			N'���������'),
(NEWID(), 654,			N'������ �����), ������ ����������), �������-��-�����'), 
(NEWID(), 580,			N'�������� ���������� �������'),
(NEWID(), 690,			N'�������'),
(NEWID(), 652,			N'���-���������'),
(NEWID(), 686,			N'�������'),
(NEWID(), 663,			N'���-������'),
(NEWID(), 534,			N'���-������ (������������� �����),'),
(NEWID(), 666,			N'���-���� � �������'),
(NEWID(), 670,			N'����-������� � ���������'),
(NEWID(), 659,			N'����-���� � �����'),
(NEWID(), 662,			N'����-�����'),
(NEWID(), 688,			N'������'),
(NEWID(), 702,			N'��������'),
(NEWID(), 760,			N'��������� �������� ����������'),
(NEWID(), 703,			N'��������'),
(NEWID(), 705,			N'��������'),
(NEWID(), 826,			N'����������� �����������'),
(NEWID(), 840,			N'����������� �����'),
(NEWID(), 090,			N'���������� �������'),
(NEWID(), 706,			N'������'),
(NEWID(), 729,			N'�����'),
(NEWID(), 740,			N'�������'),
(NEWID(), 694,			N'������-�����'),
(NEWID(), 762,			N'�����������'),
(NEWID(), 158,			N'������� (�����),'),
(NEWID(), 764,			N'�������'),
(NEWID(), 834,			N'��������(������������ ����������),'),
(NEWID(), 626,			N'�����-�����'),
(NEWID(), 768,			N'����'),
(NEWID(), 772,			N'�������'),
(NEWID(), 776,			N'�����'),
(NEWID(), 780,			N'�������� � ������'),
(NEWID(), 798,			N'������'),
(NEWID(), 788,			N'�����'),
(NEWID(), 795,			N'���������'),
(NEWID(), 792,			N'������'),
(NEWID(), 800,			N'������'),
(NEWID(), 860,			N'����������'),
(NEWID(), 804,			N'�������'),
(NEWID(), 876,			N'������ � ������'),
(NEWID(), 858,			N'�������'),
(NEWID(), 234,			N'��������� �������'),
(NEWID(), 242,			N'�����'),
(NEWID(), 608,			N'���������'),
(NEWID(), 246,			N'���������'),
(NEWID(), 238,			N'������������ ������� (�����������),'),
(NEWID(), 250,			N'�������'),
(NEWID(), 254,			N'����������� ������'),
(NEWID(), 258,			N'����������� ���������'),
(NEWID(), 260,			N'����������� ����� ����������'),
(NEWID(), 191,			N'��������'),
(NEWID(), 140,			N'����������-����������� ����������'),
(NEWID(), 148,			N'���'),
(NEWID(), 499,			N'����������'),
(NEWID(), 203,			N'������� ����������'),
(NEWID(), 152,			N'����'),
(NEWID(), 756,			N'���������'),
(NEWID(), 752,			N'������'),
(NEWID(), 744,			N'���������� � �� �����'),
(NEWID(), 144,			N'���-�����'),
(NEWID(), 218,			N'�������'),
(NEWID(), 226,			N'�������������� ������'),
(NEWID(), 248,			N'��������� �������'),
(NEWID(), 222,			N'���-���������'),
(NEWID(), 232,			N'�������'),
(NEWID(), 233,			N'�������'),
(NEWID(), 231,			N'�������'),
(NEWID(), 710,			N'����� ������'),
(NEWID(), 239,			N'����� �������� � ����� ���������� �������'), 
(NEWID(), 896,			N'����� ������'),
(NEWID(), 728,			N'����� �����'),
(NEWID(), 388,			N'������'),
(NEWID(), 392,			N'������')


update Countries
set Name = upper(left([Name],1)) + lower(substring([Name],2,999))-- ������ ������ ���������, �� ������� �� 999 ����������
go

			
INSERT INTO [NewsAggregator].[dbo].[�ities] (Id, [Name])
values		
(NEWID(),	N'����������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'����������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'����-��������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'����������'	),
(NEWID(),	N'������������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�����-�������'),
(NEWID(),	N'���������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'����'	),
(NEWID(),	N'�����������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�����������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'����'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'������� �����'	),
(NEWID(),	N'����������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'����������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'����������'	),
(NEWID(),	N'�����������'	),
(NEWID(),	N'����������'	),
(NEWID(),	N'����'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�����������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'�����	'),
(NEWID(),	N'�������'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�����	'),
(NEWID(),	N'���������'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'���������'	),
(NEWID(),	N'������ ������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'����'	),
(NEWID(),	N'��������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�������'	),
(NEWID(),	N'�����'	),
(NEWID(),	N'�����'	)
go

INSERT INTO Roles(Id,[Name], IsMember, CreateDate)
values
(NEWID(),	N'User', 1, GETDATE()),
(NEWID(),	N'Admin', 1, GETDATE()),
(NEWID(),	N'Guest', 0, GETDATE())