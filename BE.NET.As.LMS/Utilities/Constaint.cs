namespace BE.NET.As.LMS.Utilities
{
    public class Constaint
    {
        public enum EnumCourseLevel
        {
            Basic = 1,
            Advanced = 2
        }
        public enum OrderStatus
        {
            Cancel = -1,
            UnPaid = 0,
            Paid = 1
        }
        public enum CourseStatus
        {
            Public = 1,
            Hidden = 0
        }
        public static class RoleHashCode
        {
            public const string Admin = "3799D14C-03B5-461B-A6C0-AE3F8281B0B1";
            public const string Instructor = "2787DAC0-AFDF-43F5-8FBD-257B5857CA5B";
            public const string User = "1DB8AA44-AEF6-44C4-A34B-5845265DE6AE";
        }
        public static class UserHashCode
        {
            public const string Admin = "355928A0-E936-4BF0-BD08-12DF85F34979";
            public const string Instructor1 = "3670CCE4-A029-4A83-A047-BB1FE3E1D3F1";
            public const string Instructor2 = "D9E58D24-67C4-489B-8E12-4E93AF068726";
            public const string Instructor3 = "88385ACA-7D80-4B6F-8CDC-33E2E6EBFF41";
            public const string User1 = "378EF81B-BE99-489F-AD23-9990F655C7D5";
            public const string User2 = "D9DFDF78-8086-419C-8FA7-BFD5EE32167B";
            public const string User3 = "0656C94D-26ED-4E21-BBC8-A62528D5F42E";
        }
        public static class ConstHashCode
        {
            public const string Category1 = "3214928A0-E936-4BF0-BD08-12DF85F34979";
            public const string Category2 = "375AF6DE-72A0-4601-83A7-6740A7D4E0F0";
            public const string Category3 = "E10D2A57-1AF2-4EB4-84CA-77F1036F7FD7";
            public const string Category4 = "166769CE-C916-488A-A828-73D614FEE28F";
            public const string Category5 = "0A7D4CD3-32BB-4E9F-B2FD-E68D8B467C5D";
            public const string Course1 = "1234928A0-E936-4BF0-BD08-12DF85F34979";
            public const string Course2 = "3221CCE4-A029-4A83-A047-BB1FE3E1D3F1";
            public const string Course3 = "D8E64356-21B3-4D8A-820B-E101548A033D";
            public const string Course4 = "0F3B51E6-7DD9-4861-A693-53C7E258EE5E";
            public const string Course5 = "0FE71577-FF4B-4CCD-B514-B002AED2B49F";
            public const string Section1 = "321TF81B-BE99-489F-AD23-9990F655C7D5";
            public const string Section2 = "431TF81B-BE99-489F-AD23-9990F655C7D5";
            public const string Section3 = "51FE75A6-619A-49FF-A2C6-700428A48854";
            public const string Section4 = "1AB4F3A9-EA8A-4231-8DA4-3E42F768EF03";
            public const string Section5 = "6D107704-F439-469E-8B1C-B2A25440D346";
            public const string Lesson1 = "521TF81B-BE99-489F-AD23-9990F655C7D5";
            public const string Lesson2 = "631TF81B-BE99-489F-AD23-9990F655C7D5";
            public const string Lesson3 = "BA21DA23-BCB1-41BD-95BB-669F657524BF";
            public const string Lesson4 = "576225BB-C716-4F8A-8B96-51ED727DB42A";
            public const string Lesson5 = "D3B5FFF7-E24A-419A-B058-9AC26552088C";
            public const string Lesson6 = "5B4B08CB-7906-4705-9AC1-0CC5809E458E";
            public const string Lesson7 = "9467E25E-19EC-4165-B526-A01AFACA0A48";
            public const string Lesson8 = "FD468249-96A5-409E-B94F-E68B52D2AD60";
            public const string Lesson9 = "659FC410-323E-4B8A-907F-DA1F022161F2";
            public const string Lesson10 = "72501755-F532-4BB2-B14F-A83A9C1352BC";
            public const string Note1 = "731TF81B-BE99-489F-AD23-9990F655C7D5";
            public const string Note2 = "EF35932D-1A4C-4B41-A575-ECD0C8592033";
            public const string Note3 = "892BB8F5-A17D-4C55-B024-27008A19FA75";
            public const string Note4 = "5CADD68B-09D7-477C-9E01-059CAD99A99E";
            public const string Note5 = "103E2617-077C-4059-9A02-57283074BDBE";
            public const string Notification1 = "8A4EB22E-451D-4AB0-B1C1-54CEC2BFA4B4";
            public const string Notification2 = "35DAE15D-6B67-4979-A5DB-48E53950B538";
            public const string Notification3 = "AA7E120E-0DFE-449C-97C6-FCA8B2EB9CAA";
            public const string Notification4 = "52C60A16-0CDF-48B3-B033-39DC34DCC93D";
            public const string Notification5 = "CEA56C0C-818A-410D-AC2C-D58D2D943CCD";
            public const string NotificationUser1 = "B27DAAA5-AC75-41A9-B5CC-77D9053ABC3E";
            public const string NotificationUser2 = "A8AA4D8F-F7F8-4D46-85EA-286BC604BBD0";
            public const string NotificationUser3 = "6168B0D9-923D-4F81-B229-F54E6562EC1E";
            public const string NotificationUser4 = "F66CD74D-90C5-45EA-8A80-8875E18EEFF3";
            public const string NotificationUser5 = "E57E9738-523C-4123-A202-6090F75F597E";
            public const string Order1 = "AB314DBD-80D8-46DC-B1F4-0FDDC39EE741";
            public const string Order2 = "6686CD83-31C5-49B5-AC8A-354848941111";
            public const string Order3 = "1E70AF91-1C00-4DD5-8A0A-6EC4769CCE2B";
            public const string Order4 = "44ACA34B-DD8A-4AF4-94C4-739560FF1D21";
            public const string Order5 = "DADF67BA-191A-40E2-8FBC-AE0225A16B4A";
            public const string OrderDetail1 = "9F67F342-D8E5-4682-B54B-39C6C4C637F6";
            public const string OrderDetail2 = "52FD4766-7284-4476-8FC5-27B161577CA4";
            public const string OrderDetail3 = "8F78610C-EDAC-493F-9C81-CE987B61168F";
            public const string OrderDetail4 = "2BF76B2F-A3FC-4A51-94A4-27E82FA6EB25";
            public const string OrderDetail5 = "7F90E6C8-D478-424E-81AE-8588B7EE23A6";
            public const string Quiz1 = "E0B10CBD-DA37-417A-B0FF-44B3195E603D";
            public const string Quiz2 = "C4862B80-BC3C-40AC-8F3C-41FF53ABF209";
            public const string Quiz3 = "61C992E8-9551-428A-9525-6EA29C2E4468";
            public const string Quiz4 = "F8D24954-20EB-43BA-809E-098199746070";
            public const string Quiz5 = "8B31A0D0-31FC-47BC-9EFE-DBB8AAD5A35C";
            public const string QuizUser1 = "0BC9D864-02D7-423D-A64C-B03AA6331461";
            public const string QuizUser2 = "D1FE52E8-62C9-42F2-88E2-BFF5D66875C2";
            public const string QuizUser3 = "67C70C37-EE76-4084-93B2-87E63444D993";
            public const string QuizUser4 = "A00556C9-163D-4343-A59D-42F6A53B1BE2";
            public const string QuizUser5 = "DF671D71-0B59-467D-BCDF-B1BE8F4FF49F";
            public const string Answer1 = "D9FF643C-CE04-4CCF-A26B-431967D4EA24";
            public const string Answer2 = "B0687982-1590-460C-A0FA-D12D73B6B5D0";
            public const string Answer3 = "625728E7-F56A-4F01-8426-DD2DDBC83A59";
            public const string Answer4 = "6C7C1838-0C0F-4B64-92B7-530C6AAC098E";
            public const string Answer5 = "649C0E26-0834-4437-BAAF-A4ACBC12CF78";
            public const string Answer6 = "67FF1756-D8A1-4157-8E27-B26E4762B8BC";
            public const string Answer7 = "6402B5A9-1342-4BD6-8F90-0F424F1CB0FD";
            public const string Answer8 = "E0FCC671-4917-4C0D-84D9-29E4FABD0BAF";
            public const string Answer9 = "5DE494DD-A535-489B-A845-BD370DC5A611";
            public const string Answer10 = "209B8442-4305-4BB3-9442-42AC0A18EE86";
            public const string SocialMedia1 = "05938D79-D30C-443C-B065-A1EC7E581EEA";
            public const string SocialMedia2 = "864348DE-E655-4792-828F-18C8FDA8998D";
            public const string SocialMedia3 = "C9F0E904-28A6-4730-ACE2-D1758E762D21";
            public const string SocialMedia4 = "1B3795F7-560B-4712-A292-1F0B12A38E9D";
            public const string SocialMedia5 = "F480A309-E2B4-4948-904A-0556F53EB634";
            public const string SocialMedia6 = "C29FAF72-059F-4E32-99B6-29459DDF2470";
            public const string UserCourse1 = "007D5F97-57BE-43CE-81BE-86C1CD376B7A";
            public const string UserCourse2 = "3555C645-FE22-471B-BE72-9B6828C6F2B4";
            public const string UserCourse3 = "ECBD7D7F-7F04-427F-9435-344C780CB680";
            public const string UserCourse4 = "3BD97597-F69D-48BA-ADB6-9156BEB0F8E4";
            public const string UserCourse5 = "71AC38C9-4DC8-4ACA-8F04-41F795A7C7A1";
            public const string UserCourse6 = "D915D8D8-A927-4E07-A563-E9B7D6C6591A";
            public const string Assignment1 = "54EC57F9-6CC6-4581-BC24-34FA6E8391EC";
            public const string Assignment2 = "C6BAAD97-F161-4940-B44A-FDFE0282DBD3";
            public const string Assignment3 = "755AB990-AD10-42F4-9AC9-5F7D0137E1A3";
            public const string Assignment4 = "EA2DD1E1-2CED-4344-B0B6-B931AA939500";
            public const string Assignment5 = "4FBACC4A-703C-49A9-B973-29E92BDA4C6D";
            public const string Assignment6 = "CEFF1199-EAEB-4873-BF5F-AD799108ADBF";
            public const string Assignment7 = "30DA8FB2-099C-4ED3-8E0B-669BED4DAF6F";
            public const string Assignment8 = "BA369E3C-4273-4293-833E-F5475CBBF36D";
            public const string AssignmentUser1 = "6FAEC462-0F1E-4043-A3B4-DC8030965FF7";
            public const string AssignmentUser2 = "64AADA74-A7F7-45DB-B25D-726917225AAC";
            public const string AssignmentUser3 = "BBE9DEA8-BF9D-4635-96C0-9584777D9BFC";
            public const string AssignmentUser4 = "7ADD7CDB-4FF4-4E8C-A9D0-B28EF55A2EC6";
            public const string AssignmentUser5 = "CA1D5B58-335E-4902-A768-66248CE43485";
            public const string AssignmentUser6 = "ED7E0669-A242-4863-9C37-DE2518D109FA";
            public const string AssignmentUser7 = "C04974AC-82EC-4B66-811E-7196B903EB78";
            public const string AssignmentUser8 = "344FDB9C-6C6B-45B7-90A0-1899C053A4E3";
            public const string Comment1 = "A42B7705-7BAE-4358-89FF-DB6D92C65BA0";
            public const string Comment2 = "96B2E210-6C1D-413B-9676-A9436EF976AB";
            public const string Comment3 = "FF532BFC-A866-4C55-A7E1-B83BCB233F49";
            public const string Comment4 = "FAF27910-A14B-48CB-8788-9C05F784AA19";
            public const string Comment5 = "3A7C987A-9719-4145-B478-BD366B2762B2";
            public const string Comment6 = "7854EEC9-937B-45E9-B476-FEFF62BAB968";
            public const string Comment7 = "8226A0AE-C6AD-41DF-9519-A34040154A36";
            public const string Comment8 = "E7DEFD81-FB1E-4D10-979D-099D03E6DBA3";
            public const string DescriptionDetail1 = "27802CEC-BF9C-4DEA-B81F-B6755FF307C0";
            public const string DescriptionDetail2 = "0C7DA9E9-00E3-4B0C-9754-3AD8AC2CB2BA";
            public const string DescriptionDetail3 = "B9137264-D116-4C19-A04B-AD1626C9279E";
            public const string DescriptionDetail4 = "767A99BE-81D7-48C9-BF7A-1EBEA7A11206";
            public const string DescriptionDetail5 = "70954743-B945-4EFF-8CDB-99C6CAADDAC8";
        }
    }
}