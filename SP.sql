CREATE proc [dbo].[GetUserRegistrationDetailsByID]  
(@UserId int)  
as    
begin    
select * from tbl_UserRegistration nolock where UserId=@UserId order by createdDate desc    
end 

create proc [dbo].[insertUserMenuRole]
(
@LoginId int,
@MenuId nvarchar(max)
)
as 
begin

delete from UserMenuMapping where LoginId=@LoginId

insert into UserMenuMapping (MenuId,LoginId)
select value,@LoginId
FROM STRING_SPLIT(@MenuId, ',');

select 'Menu Updated successfully.'
end

CREATE PROC [dbo].[usp_AdminUserLogin]           
@EmailId VARCHAR(500),            
@Password VARCHAR(500)            
AS            
BEGIN            
SELECT U.*,R.RoleName     
FROM tbl_UserRegistration (NOLOCK) U      
inner join tbl_role R on R.id=U.role      
WHERE u.emailId = @EmailId         
AND u.isActive=1        
AND password = @Password COLLATE Latin1_General_BIN          
         
END 

CREATE PROCEDURE [dbo].[usp_ChangePassword]       
 @LoginId int,          
 @EmailId varchar(100),          
 @OldPassword varchar(100),          
 @Password varchar(100)          
AS          
BEGIN          
if exists(select *from tbl_UserRegistration nolock where userId=@LoginId AND emailId=@EmailId and password=@OldPassword  COLLATE Latin1_General_BIN and isActive=1)      
begin      
  UPDATE tbl_UserRegistration          
  SET password=@Password, modifiedDate=GETDATE()          
  WHERE userId=@LoginId AND emailId=@EmailId and password=@OldPassword  COLLATE Latin1_General_BIN and isActive=1        
  select 'Password changed successfully.,' + (select name from tbl_UserRegistration nolock where userId=@LoginId) as Msg    
end      
else      
begin      
Select 'Old password is invalid' as Msg    
end      
END 

CREATE PROC [dbo].[usp_CheckUser]               
@EmailId VARCHAR(500) ,
@Password VARCHAR(500)
AS                
BEGIN                
  
if exists(SELECT U.*,R.RoleName         
FROM tbl_UserRegistration (NOLOCK) U          
inner join tbl_role R on R.id=U.role          
WHERE u.emailId = @EmailId             
AND u.isActive=1    )  
begin  
select 'Valid,' +  (select U.name FROM tbl_UserRegistration (NOLOCK) U          
inner join tbl_role R on R.id=U.role          
WHERE u.emailId = @EmailId             
AND u.isActive=1) 

update U
set Password=@Password
FROM tbl_UserRegistration (NOLOCK) U          
inner join tbl_role R on R.id=U.role          
WHERE u.emailId = @EmailId             
AND u.isActive=1 
end  
else  
begin  
select 'Invalid'  
end  
             
END   
  
create  proc [dbo].[usp_get_EmpMenuMapping] 
(
@LoginId int =null
)
as 
begin
select id,MenuId from  UserMenuMapping where LoginId=@LoginId
end

create proc [dbo].[usp_get_MenuDetails]
as 
begin
select * from MenuMaster where IsVisible=1
end

create procedure [dbo].[usp_get_role]  
as begin  
select * from tbl_role where isactive=1 order by rolename  
end

CREATE proc [dbo].[usp_get_UserRegistraionDetails]

as 
begin
select userId,name from tbl_UserRegistration where isActive=1
end

CREATE proc [dbo].[usp_GetUserRegistrationDetails]    
as    
begin    
select u.*,r.rolename from tbl_UserRegistration U  
inner join tbl_role R on r.id=u.role  
order by createdDate desc    
end 

CREATE PROCEDURE [dbo].[USP_INSERTUSERREGISTER]       
@ACTION VARCHAR(100),       
@USERID INT,      
@NAME VARCHAR(100),            
@EMAILID VARCHAR(100),            
@PASSWORD VARCHAR(100),            
@MOBILE VARCHAR(100),          
@IMAGEPATH VARCHAR(MAX),    
@Role VARCHAR(100)       
            
AS            
BEGIN           
IF(@ACTION='I')      
BEGIN      
IF EXISTS(SELECT 1 FROM TBL_USERREGISTRATION NOLOCK WHERE EMAILID=@EMAILID)        
BEGIN         
 SELECT 'Emailid Already Used' AS MSG        
END        
ELSE        
BEGIN        
 INSERT INTO TBL_USERREGISTRATION (NAME,EMAILID,PASSWORD,MOBILENO,ISACTIVE,ROLE,CREATEDDATE,IMAGEPATH)            
 VALUES (@NAME,@EMAILID,@PASSWORD,@MOBILE,1,@Role--(select rolename from tbl_role where id=@Role)  
 ,GETDATE(),@IMAGEPATH)        
       
 SELECT 'User Added Successfully' AS MSG        
      
END          
END      
ELSE      
BEGIN      
IF EXISTS(SELECT 1 FROM TBL_USERREGISTRATION NOLOCK WHERE EMAILID=@EMAILID AND USERID != @USERID)        
BEGIN         
 SELECT 'Emailid Already Used' AS MSG        
END        
ELSE        
BEGIN        
 UPDATE TBL_USERREGISTRATION       
 SET NAME=@NAME,      
 EMAILID=@EMAILID,      
 PASSWORD=@PASSWORD,      
 MOBILENO=@MOBILE,      
 IMAGEPATH=@IMAGEPATH,    
 [Role]=@Role--(select rolename from tbl_role where id=@Role)    
 WHERE USERID = @USERID      
      
  SELECT 'User Update Successfully' AS MSG        
      
END          
END      
END

CREATE PROC [dbo].[usp_Operation_User]      
    @Action varchar(100),  
    @UserId INT      
AS      
BEGIN     
if(@Action='InActive')  
begin  
 update tbl_UserRegistration set isActive=1 where  userId = @UserId    
 select 'Activated Successfully' as Msg  
end  
else if(@Action='Active')  
begin  
    --DELETE FROM tbl_UserRegistration WHERE userId = @UserId       
 update tbl_UserRegistration set isActive=0 where  userId = @UserId     
 select 'Deactivated Successfully' as Msg  
END   
  
end

CREATE proc [dbo].[usp_add_contactus]
(  
@Name varchar(max),  
@Email varchar(max),  
@Subject varchar(max),  
@Message varchar(max)  
)  
as  
begin  
insert into tbl_ContactUs(Name,Email,Subject,Message)  
values(@Name,@Email,@Subject,@Message)  
  
select 'Thank you for contacting us. Our team will connect with you shortly.'  
end

CREATE procedure [dbo].[usp_add_course]            
(            
@UniversityId int ,            
@RTO_ID varchar(500) ,            
@CourseName varchar(500) ,            
@CourseProvider varchar(500) ,            
@AboutCourse varchar(1000) ,            
@StudyMethod varchar(1000) ,            
@CourseCategory varchar(500) ,            
@CoursePhoto varchar(500) ,            
@CourseVideo varchar(500) ,      
@CourseVideo2 varchar(500) ,   
@CourseVideo3 varchar(500) ,   
@CourseStartDate varchar(50) ,            
@CourseDuration varchar(50) ,            
@CourseOverview varchar(1000) ,            
@CourseOutcomes varchar(1000) ,            
@CourseLearn varchar(1000) ,            
@CourseRequirements varchar(1000) ,            
@WorkPlacement varchar(1000) ,            
@RPL varchar(1000) ,            
@CourseGuide varchar(500),
@TrendingCourse varchar(500),
@CourseLevel varchar(500)
)            
as begin       
    
if (isnull(@CoursePhoto,'')='' )    
begin    
set @CoursePhoto='Uploads/NoImage.jpg'    
end    
insert into tbl_CourseDetails            
(            
UniversityId,RTO_ID,CourseName,CourseProvider,AboutCourse,StudyMethod,CourseCategory,CoursePhoto,            
CourseVideo,CourseStartDate,CourseDuration,CourseOverview,CourseOutcomes,CourseLearn,CourseRequirements,WorkPlacement,            
RPL,CourseGuide,[CourseVideo2],[CourseVideo3],CourseLevel,TrendingCourse 
)            
values            
(            
@UniversityId,@RTO_ID,@CourseName,@CourseProvider,@AboutCourse,@StudyMethod,@CourseCategory,@CoursePhoto,            
@CourseVideo,@CourseStartDate,@CourseDuration,@CourseOverview,@CourseOutcomes,@CourseLearn,@CourseRequirements,@WorkPlacement,            
@RPL,@CourseGuide,@CourseVideo2,@CourseVideo3, @CourseLevel , @TrendingCourse   
)            
            
select 'Course Added,'+ convert(varchar,(SELECT SCOPE_IDENTITY()))  +','+(select Legal_Name_Organisation from tbl_university_details where Id=@UniversityId) +','+(select General_Contact_Email from tbl_university_details where Id=@UniversityId)as msg      
  
    
      
end      
      

      CREATE procedure [dbo].[usp_add_student]      
(      
@FirstName varchar(500),      
@MiddleName varchar(500),      
@LastName varchar(500),      
@EmailId varchar(500),      
@Mobile varchar(20),      
@Password varchar(50),    
@isAustraliaResident int,    
@Zipcode varchar(5000), 
@Area varchar(1000),
@State varchar(5000),    
@Country varchar(5000)    
)      
as begin      
      
if exists(select 1 from tbl_student_details nolock where EmailId=@EmailId)      
 begin      
  select 'Email Id already exist' as msg      
 end      
else      
 begin      
 declare @Verifylink nvarchar(1000)=NEWID();  
  insert into tbl_student_details      
  (FirstName,MiddleName,LastName,EmailId,Mobile,Password,isAustraliaResident,Zipcode,Area,State,Country,VerifyLink)      
  values      
  (@FirstName,@MiddleName,@LastName,@EmailId,@Mobile,@Password,@isAustraliaResident,@Zipcode,@Area,@State,@Country,@Verifylink)      
  select 'Student Added Successfully,'+ @Verifylink as msg      
 end      
      
end 

CREATE procedure [dbo].[usp_add_testimonial]        
(        
@Course_Id int,  
@Student_Id int=null,  
@Person_Name varchar(500)=null,        
@Designation varchar(500)=null,        
@Comment varchar(5000)=null        
)        
as begin        
insert into tbl_CourseTestimonial         
(Course_Id,Person_Name,Designation,Comment,Student_Id)        
values        
(@Course_Id,@Person_Name,@Designation,@Comment,@Student_Id)        
select 'Thank you for your feedback' as msg    
end   

CREATE procedure [dbo].[usp_add_University]  
(  
@RTO_ID varchar(500),  
@RTO_Status int ,  
@CRICOS_Code varchar(500) ,  
@Legal_Name_Organisation varchar(500) ,  
@Trading_Name varchar (500) ,  
@ABN_ACN varchar(50) ,  
@Business_Address varchar(500) ,  
@Postal_Address varchar(500) ,  
@Website varchar(500) ,  
@Contact_Number varchar(50) ,  
@General_Contact_Email varchar(50) ,  
@Full_Name varchar(500) ,  
@Position varchar(500) ,  
@Direct_Phone varchar(50) ,  
@Email varchar(500) ,  
@Relevant_Courses varchar(500) ,  
@Signature varchar(500) ,  
@Logo varchar(500) ,  
@Password varchar(50) ,
@ZipCode varchar(500),
@State varchar(500)
)  
as begin  
  
if exists(select 1 from tbl_university_details nolock where Email=@Email)  
 begin  
  select 'Email Id already exist' as msg  
 end  
else  
 begin  
  insert into tbl_university_details  
  (  
  RTO_ID ,RTO_Status,CRICOS_Code ,Legal_Name_Organisation ,  
  Trading_Name ,ABN_ACN,Business_Address ,Postal_Address ,  
  Website ,Contact_Number,General_Contact_Email,  
  Full_Name ,Position ,Direct_Phone,Email ,  
  Relevant_Courses ,Signature ,Logo ,Password  ,ZipCode,State
  )  
  values  
  (  
  @RTO_ID ,@RTO_Status,@CRICOS_Code ,@Legal_Name_Organisation ,  
  @Trading_Name ,@ABN_ACN,@Business_Address ,@Postal_Address ,  
  @Website ,@Contact_Number,@General_Contact_Email,@Full_Name ,  
  @Position ,@Direct_Phone,@Email ,@Relevant_Courses ,  
  @Signature ,@Logo ,@Password  ,@ZipCode,@State
  )  
  select 'University Added Successfully' as msg  
 end  
  
end

CREATE proc [dbo].[usp_AddBlogs]    
(    
@BlogTitle varchar(max),    
@OnelinerDiscription varchar(max),    
@Quote varchar(max),    
@FullDiscription varchar(max),    
@SubTitle varchar(max),    
@SubTitleDiscription  varchar(max),    
@BlogThumbnail varchar(max),    
@BlogSmallThumbnail1 varchar(max),    
@BlogSmallThumbnail2 varchar(max),    
@BlogSmallThumbnail3 varchar(max),  
@CreatedBy varchar(max)  
)    
as     
begin    
insert into tbl_Blogs (BlogTitle    
,OnelinerDiscription    
,Quote    
,FullDiscription    
,SubTitle    
,SubTitleDiscription    
,BlogThumbnail    
,BlogSmallThumbnail1    
,BlogSmallThumbnail2    
,BlogSmallThumbnail3
,BlogVerified
,CreatedBy)    
values    
(@BlogTitle,    
@OnelinerDiscription,    
@Quote,    
@FullDiscription,    
@SubTitle,    
@SubTitleDiscription,    
@BlogThumbnail,    
@BlogSmallThumbnail1,    
@BlogSmallThumbnail2,    
@BlogSmallThumbnail3,
1,
@CreatedBy)    
    
select 'Blog Saved Successfully!!'    
end

CREATE procedure [dbo].[usp_change_status_course]            
(            
@Course_Id int,    
@IsActive int    
)            
as begin       
      
 update tbl_CourseDetails           
 set isReject=0,RejectionReason='', IsActive=@IsActive where Id=  @Course_Id          
    
 update tbl_CourseTestimonial           
 set IsActive=@IsActive where Course_Id=  @Course_Id          
      
 select case when a.IsActive=0 then 'Inactivated' else 'Activated' end  +','+a.CourseName+','+ b.Legal_Name_Organisation+','+b.General_Contact_Email as msg  
 from tbl_CourseDetails(nolock) a  
 left join tbl_university_details(nolock) b on a.UniversityId=b.Id  
 where a.Id = @Course_Id          
       
end  

CREATE procedure [dbo].[usp_change_status_student]     
(      
@Id int,      
@IsActive int      
)      
as begin      
      
 update tbl_student_details      
 set      
 IsActive=@IsActive      
 where Id=@Id      
   
 select case when IsActive=0 then 'Inactived' else 'Actived' end + ','+ FirstName+ ' '+ LastName +','+EmailId as msg         
 from tbl_student_details with(nolock) where Id = @Id           
      
end 

CREATE procedure [dbo].[usp_change_status_university]   --1,1            
(                
@Id int,                
@IsActive int,
@IsReject int,
@RejectionReason varchar(500)
)                
as begin   
if(@IsActive=1)
begin 

    DECLARE @chars VARCHAR(36) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';      
    DECLARE @result VARCHAR(7) = '';      
    DECLARE @i INT = 1;      
      
    WHILE @i <= 7      
    BEGIN      
        SET @result += SUBSTRING(@chars, ABS(CHECKSUM(NEWID())) % LEN(@chars) + 1, 1);      
        SET @i += 1;      
    END 
    
    update tbl_university_details                
    set                
    IsActive=@IsActive,      
    [Password]= @result   
    where Id=@Id                
              
    update tbl_CourseDetails                       
    set IsActive=@IsActive where UniversityId=@Id              
                
    update tbl_CourseTestimonial                       
    set IsActive=@IsActive where Course_Id in (select distinct Id from tbl_CourseDetails nolock where UniversityId=@Id )              
              
end
if(@IsActive=0)
begin

    update tbl_university_details                
    set                
    IsActive=@IsActive     
    where Id=@Id                
              
    update tbl_CourseDetails                       
    set IsActive=@IsActive 
    where UniversityId=@Id              
                
    update tbl_CourseTestimonial                       
    set IsActive=@IsActive 
    where Course_Id in (select distinct Id from tbl_CourseDetails nolock where UniversityId=@Id ) 
    
end
if(@IsReject=1)
begin
    update tbl_university_details                
    set
    IsActive=@IsActive,
    IsReject=@IsReject,
    RejectionReason=@RejectionReason
    where Id=@Id                
              
    update tbl_CourseDetails                       
    set IsActive=@IsActive 
    where UniversityId=@Id              
                
    update tbl_CourseTestimonial                       
    set IsActive=@IsActive 
    where Course_Id in (select distinct Id from tbl_CourseDetails nolock where UniversityId=@Id ) 
end
 
 select case when IsActive=0 then 'Inactived' else 'Actived' end +','+case when isnull(IsReject,0)=0 then 'Approved' else 'Reject' end+','+Legal_Name_Organisation+','+General_Contact_Email+','+[Password] as msg                   
 from tbl_university_details with(nolock) where Id = @Id                     
                
end 

CREATE procedure [dbo].[usp_change_status_university_back]   --1,1              
(                  
@Id int,                  
@IsActive int,  
@IsReject int,  
@RejectionReason varchar(500)  
)                  
as begin     
if(@IsActive=1)  
begin   
  
    DECLARE @chars VARCHAR(36) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';        
    DECLARE @result VARCHAR(7) = '';        
    DECLARE @i INT = 1;        
        
    WHILE @i <= 7        
    BEGIN        
        SET @result += SUBSTRING(@chars, ABS(CHECKSUM(NEWID())) % LEN(@chars) + 1, 1);        
        SET @i += 1;        
    END   
      
    update tbl_university_details                  
    set                  
    IsActive=@IsActive,        
    [Password]= @result     
    where Id=@Id                  
                
    update tbl_CourseDetails                         
    set IsActive=@IsActive where UniversityId=@Id                
                  
    update tbl_CourseTestimonial                         
    set IsActive=@IsActive where Course_Id in (select distinct Id from tbl_CourseDetails nolock where UniversityId=@Id )                
                
end  
if(@IsActive=0)  
begin  
  
    update tbl_university_details                  
    set                  
    IsActive=@IsActive       
    where Id=@Id                  
                
    update tbl_CourseDetails                         
    set IsActive=@IsActive   
    where UniversityId=@Id                
                  
    update tbl_CourseTestimonial                         
    set IsActive=@IsActive   
    where Course_Id in (select distinct Id from tbl_CourseDetails nolock where UniversityId=@Id )   
      
end  
if(@IsReject=1)  
begin  
    update tbl_university_details                  
    set  
    IsActive=@IsActive,  
    IsReject=@IsReject,  
    RejectionReason=@RejectionReason  
    where Id=@Id                  
                
    update tbl_CourseDetails                         
    set IsActive=@IsActive   
    where UniversityId=@Id                
                  
    update tbl_CourseTestimonial                         
    set IsActive=@IsActive   
    where Course_Id in (select distinct Id from tbl_CourseDetails nolock where UniversityId=@Id )   
end  
   
 select case when IsActive=0 then 'Inactived' else 'Actived' end +','+case when isnull(IsReject,0)=0 then 'Approved' else 'Reject' end+','+Legal_Name_Organisation+','+General_Contact_Email+','+[Password] as msg                     
 from tbl_university_details with(nolock) where Id = @Id                       
                  
end   

CREATE procedure [dbo].[usp_change_status_university_ver1]   --1,1                  
(                      
@Id int,                      
@IsActive int,      
@IsReject int,      
@IsUniversityActive int,    
@IsDelete int,    
@RejectionReason varchar(500)      
)                      
as begin         
if(@IsDelete=1)    
begin    
insert into tbl_university_details_archive (UnivercityId,    
RTO_ID,RTO_Status,CRICOS_Code,Legal_Name_Organisation,Trading_Name,ABN_ACN,Business_Address,Postal_Address,    
Website,Contact_Number,General_Contact_Email,Full_Name,Position,Direct_Phone,Email,Relevant_Courses,Signature,    
Logo,Password,IsActive,CreateDate,ZipCode,State,IsReject,RejectionReason)    
select Id,    
RTO_ID,RTO_Status,CRICOS_Code,Legal_Name_Organisation,Trading_Name,ABN_ACN,Business_Address,Postal_Address,    
Website,Contact_Number,General_Contact_Email,Full_Name,Position,Direct_Phone,Email,Relevant_Courses,Signature,    
Logo,Password,IsActive,CreateDate,ZipCode,State,IsReject,RejectionReason from tbl_university_details where Id=@Id    
    
insert into [tbl_CourseDetails_archive]
(CourseId,UniversityId,RTO_ID,CourseName,CourseProvider,AboutCourse,StudyMethod,CourseCategory,CoursePhoto,
CourseVideo,CourseStartDate,CourseDuration,CourseOverview,CourseOutcomes,CourseLearn,
CourseRequirements,WorkPlacement,RPL,CourseGuide,CreatedDate,IsActive,isReject,RejectionReason,
CourseVideo2,CourseVideo3,TrendingCourse,CourseLevel)
select Id,UniversityId,RTO_ID,CourseName,CourseProvider,AboutCourse,StudyMethod,CourseCategory,CoursePhoto,
CourseVideo,CourseStartDate,CourseDuration,CourseOverview,CourseOutcomes,CourseLearn,
CourseRequirements,WorkPlacement,RPL,CourseGuide,CreatedDate,IsActive,isReject,RejectionReason,
CourseVideo2,CourseVideo3,TrendingCourse,CourseLevel 
from tbl_CourseDetails where UniversityId=@Id

insert into tbl_CourseTestimonial_archive (Course_Id,Person_Name,Designation,Comment,IsActive,CreatedDate,Student_Id)
select Id,Person_Name,Designation,Comment,IsActive,CreatedDate,Student_Id from tbl_CourseTestimonial 
where Course_Id in (select id from tbl_CourseDetails where UniversityId=@Id) 

select Legal_Name_Organisation+','+General_Contact_Email+','+[Password] as msg from tbl_university_details with(nolock) where Id = @Id        
delete from tbl_university_details where Id=@Id    
delete from tbl_CourseDetails where     UniversityId=@Id
delete from tbl_CourseTestimonial where  Course_Id in (select id from tbl_CourseDetails where UniversityId=@Id) 

return    
end    
if(@IsActive=1)      
begin       
      
DECLARE @chars VARCHAR(36) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';            
DECLARE @result VARCHAR(7) = '';            
DECLARE @i INT = 1;            
            
WHILE @i <= 7            
BEGIN            
SET @result += SUBSTRING(@chars, ABS(CHECKSUM(NEWID())) % LEN(@chars) + 1, 1);            
SET @i += 1;            
END       
          
update tbl_university_details                      
set                      
IsActive=@IsActive,            
[Password]= @result         
where Id=@Id                      
end      
else if(@IsReject=1)      
begin      
update tbl_university_details                      
set      
IsReject=@IsReject,      
RejectionReason=@RejectionReason      
where Id=@Id                      
end      
else if(isnull(@IsUniversityActive,5)<>5)    
begin    
update tbl_university_details    
set isUniversityActive=@IsUniversityActive    
where Id=@Id    
end    
       
select Legal_Name_Organisation+','+General_Contact_Email+','+[Password] as msg from tbl_university_details with(nolock) where Id = @Id                           
                      
end 

CREATE proc [dbo].[usp_ChangePassword]  
(  
@Id int,  
@OldPassword varchar(500),  
@Password varchar(500),  
@LoginType varchar(500)  
)  
as begin  
if(@LoginType='U')  
begin   
if exists (select * from tbl_university_details nolock where Id=@Id and Password=@OldPassword)  
begin  
Update tbl_university_details  
set Password=@Password  
where Id=@Id and Password=@OldPassword  
select 'Password Update Successfully' as Msg  
end  
else  
begin  
select 'User not found' as Msg  
end  
end  
else if(@LoginType='A')  
begin   
if exists (select * from tbl_UserRegistration nolock where userId=@Id and password=@OldPassword)  
begin  
Update tbl_UserRegistration  
set password=@Password  
where userId=@Id and password=@OldPassword  
select 'Password Update Successfully' as Msg  
end  
else  
begin  
select 'User not found' as Msg  
end  
end

else  
begin   
if exists (select * from tbl_student_details nolock where Id=@Id and Password=@OldPassword)  
begin  
Update tbl_student_details  
set Password=@Password  
where Id=@Id and Password=@OldPassword  
select 'Password Update Successfully' as Msg  
end  
else  
begin  
select 'User not found' as Msg  
end  
end  
  
end

create proc [dbo].[usp_CheckRTOID]
(
@RTOID varchar(max)
)
as
begin
if exists(select * from tbl_university_details(nolock) where RTO_ID=@RTOID)
begin
select 'Already exist'
end
else
begin
select 'Not Available'
end
end

CREATE procedure [dbo].[usp_CreateLead]        
(        
@Course_ID int,        
@Student_ID int,      
@University_ID int      
)        
as begin        
if exists(select 1 from tbl_Premium_Lead nolock where Course_ID=@Course_ID and @Student_ID=Student_ID)        
begin         
select 'You already applied for this course' as msg        
end        
else        
begin        
insert into tbl_Premium_Lead        
(Course_ID,Student_ID,University_ID)        
values        
(@Course_ID,@Student_ID,@University_ID)        
      
insert into tbl_Lead        
(Course_ID,Student_ID,University_ID)       
SELECT distinct @Course_ID,@Student_ID,U.Id      
--FROM tbl_university_details U      
--JOIN tbl_CourseDetails C ON C.CourseCategory = @Course_ID      
--JOIN tbl_category_master CM ON CM.Id = C.CourseCategory      
--WHERE U.Id<>@University_ID and U.Relevant_Courses LIKE CONCAT('%', CM.CategoryValue, '%');      
  
FROM tbl_university_details U  
JOIN tbl_CourseDetails C   
      ON C.CourseCategory = (SELECT CourseCategory from tbl_CourseDetails where Id=@Course_ID)  
JOIN tbl_category_master CM   
      ON CM.Id = C.CourseCategory  
CROSS APPLY STRING_SPLIT(U.Relevant_Courses, ',') AS RC  
WHERE U.Id <> @University_ID  
  AND RC.value = CAST(CM.Id AS VARCHAR(10));     
   
      
select 'Thank you for apply' as msg        
end        
end  

create proc [dbo].[usp_delete_Course]
(@Id int)
as begin
delete from tbl_CourseDetails where Id=@Id

select 'Deleted' as msg
end

create proc [dbo].[usp_delete_student]
(@Id int)
as begin
delete from tbl_student_details where Id=@Id

select 'Deleted' as msg
end

create proc [dbo].[usp_delete_Testimonial]
(@Id int)
as begin
delete from tbl_CourseTestimonial where Id=@Id

select 'Deleted' as msg
end

CREATE proc [dbo].[usp_delete_university]  
(@Id int)  
as begin  
delete from tbl_university_details where Id=@Id  
delete from tbl_CourseTestimonial where Course_Id in (select Id from tbl_CourseDetails where UniversityId=@Id )

delete from tbl_CourseDetails where UniversityId=@Id  
  
select 'Deleted' as msg  
end  

CREATE proc [dbo].[usp_DeleteBlog]
(
@id int
)
as
begin
delete from tbl_Blogs where id=@id;

select 'Blog Deleted Successfully!!!'
end

CREATE procedure [dbo].[usp_edit_course]        
(        
@Id int,        
@UniversityId int ,        
@RTO_ID varchar(500),        
@CourseName varchar(500) ,        
@CourseProvider varchar(500) ,        
@AboutCourse varchar(1000) ,        
@StudyMethod varchar(1000),        
@CourseCategory varchar(500) ,        
@CoursePhoto varchar(500) ,        
@CourseVideo varchar(500) ,     
@CourseVideo2 varchar(500) ,     
@CourseVideo3 varchar(500) ,     
@CourseStartDate varchar(50) ,        
@CourseDuration varchar(50) ,        
@CourseOverview varchar(1000) ,        
@CourseOutcomes varchar(1000) ,        
@CourseLearn varchar(1000) ,        
@CourseRequirements varchar(1000) ,        
@WorkPlacement varchar(1000) ,        
@RPL varchar(1000),        
@CourseGuide varchar(500),  
@TrendingCourse varchar(500),  
@CourseLevel varchar(500)    
)        
as begin        
        
UPDATE tbl_CourseDetails        
   SET         
   UniversityId = @UniversityId,         
   RTO_ID = @RTO_ID,         
   CourseName = @CourseName,         
   CourseProvider = @CourseProvider,         
   AboutCourse = @AboutCourse,         
   StudyMethod = @StudyMethod,         
   CourseCategory = @CourseCategory,         
   CoursePhoto = @CoursePhoto,         
   CourseVideo = @CourseVideo,         
   CourseStartDate = @CourseStartDate,         
   CourseDuration = @CourseDuration,         
   CourseOverview = @CourseOverview,         
   CourseOutcomes = @CourseOutcomes,         
   CourseLearn = @CourseLearn,         
   CourseRequirements = @CourseRequirements,         
   WorkPlacement = @WorkPlacement,         
   RPL = @RPL,         
   CourseGuide = @CourseGuide,    
   [CourseVideo2]=@CourseVideo2,    
   [CourseVideo3]=@CourseVideo3,  
   TrendingCourse=@TrendingCourse,  
   CourseLevel=@CourseLevel  
 WHERE Id=@Id        
 select 'Course Updated' as msg        
        
end

CREATE procedure [dbo].[usp_edit_student]  
(  
@Id int,  
@FirstName varchar(500),  
@MiddleName varchar(500),  
@LastName varchar(500),  
@EmailId varchar(500),  
@Mobile varchar(20),  
@Password varchar(50),
@isAustraliaResident int,
@Zipcode varchar(5000),
@State varchar(5000),
@Country varchar(5000)  
)  
as begin  
  
 update tbl_student_details  
 set  
 FirstName=@FirstName,  
 MiddleName=@MiddleName,  
 LastName=@LastName,  
 EmailId=@EmailId,  
 Mobile=@Mobile,  
 Password=@Password,
 isAustraliaResident=@isAustraliaResident ,
 Zipcode=@Zipcode ,
 State=@State ,
 Country=@Country 
 where Id=@Id  
 select 'Student Updated' as msg  
  
end

CREATE procedure [dbo].[usp_edit_testimonial]  
(  
@Id int,  
@Course_Id int,
@Student_Id int,
@Person_Name varchar(500),  
@Designation varchar(500),  
@Comment varchar(5000),  
@IsActive int  
)  
as begin  
update tbl_CourseTestimonial   
set 
--Course_Id=@Course_Id,  
--Person_Name=@Person_Name,  
--Designation=@Designation,  
Comment=@Comment  
--IsActive=@IsActive  
where Id=@Id  
  
end

create procedure [dbo].[usp_edit_university]
(
@Id int,
@RTO_ID varchar(500),
@RTO_Status int,
@CRICOS_Code varchar(500),
@Legal_Name_Organisation varchar(500),
@Trading_Name varchar(500),
@ABN_ACN varchar(50),
@Business_Address varchar(500),
@Postal_Address varchar(500),
@Website varchar(500),
@Contact_Number varchar(50),
@General_Contact_Email varchar(50),
@Full_Name varchar(500),
@Position varchar(500),
@Direct_Phone varchar(50),
@Email varchar(500),
@Relevant_Courses varchar(500),
@Signature varchar(500),
@Logo varchar(500),
@Password varchar(50)
)
as begin

	update tbl_university_details
	set
	RTO_ID=@RTO_ID,
	RTO_Status=@RTO_Status,
	CRICOS_Code=@CRICOS_Code,
	Legal_Name_Organisation=@Legal_Name_Organisation,
	Trading_Name=@Trading_Name,
	ABN_ACN=@ABN_ACN,
	Business_Address=@Business_Address,
	Postal_Address=@Postal_Address,
	Website=@Website,
	Contact_Number=@Contact_Number,
	General_Contact_Email=@General_Contact_Email,
	Full_Name=@Full_Name,
	Position=@Position,
	Direct_Phone=@Direct_Phone,
	Email=@Email,
	Relevant_Courses=@Relevant_Courses,
	Signature=@Signature,
	Logo=@Logo,
	Password=@Password
	where Id=@Id
	select 'University Updated' as msg

end

CREATE proc [dbo].[usp_EditBlogs]      
(     
@Id int,  
@BlogTitle varchar(max),      
@OnelinerDiscription varchar(max),      
@Quote varchar(max),      
@FullDiscription varchar(max),      
@SubTitle varchar(max),      
@SubTitleDiscription  varchar(max),      
@BlogThumbnail varchar(max)=null,      
@BlogSmallThumbnail1 varchar(max)=null,      
@BlogSmallThumbnail2 varchar(max)=null,      
@BlogSmallThumbnail3 varchar(max)=null,    
@ModifyBy varchar(max)    
)      
as       
begin      
update  tbl_Blogs   
  
set BlogTitle = @BlogTitle   
,OnelinerDiscription = @OnelinerDiscription     
,Quote = @Quote  
,FullDiscription = @FullDiscription    
,SubTitle = @SubTitle  
,SubTitleDiscription = @SubTitleDiscription     
,BlogThumbnail = @BlogThumbnail  
,BlogSmallThumbnail1 = @BlogSmallThumbnail1  
,BlogSmallThumbnail2 = @BlogSmallThumbnail2    
,BlogSmallThumbnail3 = @BlogSmallThumbnail3  
,ModifyBy =@ModifyBy  
,ModifyDate=getdate()  
where id=@Id  
  
  
  
  
      
select 'Blog Updated Successfully!!'      
end

CREATE proc [dbo].[usp_get_applied_course] --1
(
@StudentId int
)
as begin

select C.Id,U.Legal_Name_Organisation as UniversityName,C.CourseName,U.General_Contact_Email as UniversityEmail from tbl_Premium_Lead L with(nolock) 
inner join tbl_CourseDetails C with(nolock) on C.Id=L.Course_ID
inner join tbl_university_details U with(nolock) on U.Id=L.University_ID
where Student_ID=@StudentId order by L.createddate desc

end

CREATE procedure [dbo].[usp_get_ArchiveUniversity]      
(@UniversityId int=null)
as begin                      
 select                   
 U.Id,RTO_ID,RTO_Status,CRICOS_Code,Legal_Name_Organisation,Trading_Name,ABN_ACN,Business_Address,                  
 Postal_Address,Website,Contact_Number,General_Contact_Email,Full_Name,Position,Direct_Phone,Email,                  
 --Relevant_Courses,              
 Signature,                  
 Logo,Password,U.IsActive,U.CreateDate,U.IsReject,U.RejectionReason ,isUniversityActive                 
 from tbl_university_details_archive U with(nolock)     --where IsActive=1      
 WHERE (@UniversityId IS NULL OR U.Id = @UniversityId)
 order by U.CreateDate desc  
end

CREATE procedure [dbo].[usp_get_category]    
(    
    @UniversityId varchar(500) = NULL    
)    
as begin    
    SET NOCOUNT ON;    
    
    SELECT DISTINCT C.*    
    FROM tbl_category_master C WITH (NOLOCK)    
    LEFT JOIN tbl_university_details U WITH (NOLOCK)    
        ON U.Id = @UniversityId    
    OUTER APPLY STRING_SPLIT(U.Relevant_Courses, ',') AS S    
    WHERE C.IsActive = 1    
      AND (@UniversityId IS NULL OR C.Id = TRY_CAST(S.value AS INT))
	  order by CategoryValue;   
end

CREATE PROCEDURE [dbo].[usp_get_category_new]
(
    @UniversityId INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT C.*
    FROM tbl_category_master C WITH (NOLOCK)
    LEFT JOIN tbl_university_details U WITH (NOLOCK)
        ON U.Id = @UniversityId
    OUTER APPLY STRING_SPLIT(U.Relevant_Courses, ',') AS S
    WHERE C.IsActive = 1
      AND (@UniversityId IS NULL OR C.Id = TRY_CAST(S.value AS INT));
END

CREATE procedure [dbo].[usp_get_categorytype]
as begin  
select distinct CategoryType from tbl_category_master nolock where IsActive=1  
end


CREATE procedure [dbo].[usp_get_course]  --0,'',null,null
(                                          
@Id int,                                
@LoginType varchar(50),                            
@CategoryType varchar(50)=null,                        
@CategoryId varchar(50)=null,                       
@UniversityId varchar(50)=null,  
@StudeyType  varchar(50)=null,  
@State  varchar(500)=null,  
@ZipCode  varchar(50)=null,  
@CourseLevel  varchar(50)=null,  
@QualificationType varchar(50)=null  
)                                          
as begin                                          
if(@Id<>0)                                          
select              
C.Id,UniversityId,U.Legal_Name_Organisation as UniversityName,C.RTO_ID,C.CourseName,CourseProvider,AboutCourse,StudyMethod,                                  
CourseCategory, CategoryValue as CategoryName,CoursePhoto,                                    
CourseVideo,CourseStartDate,CourseDuration,CourseOverview,CourseOutcomes,                                    
CourseLearn,CourseRequirements,                                    
WorkPlacement,RPL,CourseGuide,C.CreatedDate,C.IsActive,Category.CategoryType,isnull(C.isReject,0) as IsReject,C.RejectionReason, TD.CourseName 'TrendingCoursesName'  ,CL.CourseLevel 'CourseLevelName'                               
,U.Logo as UniversityLogo    
from tbl_CourseDetails C with(nolock)                                   
inner join tbl_university_details U with(nolock) on C.UniversityId=U.Id                             
left join tbl_category_master Category with(nolock) on C.CourseCategory=Category.Id                 
left join tbl_TrendingCourses_Master TD with(nolock) on C.TrendingCourse=TD.Id                
left join  tbl_CourseLevel_Master CL with(nolock) on C.CourseLevel=CL.Id                
where C.Id=@Id                                 
AND (                                
( @LoginType = 'Admin' or  @LoginType = 'University' )                                
OR C.IsActive = 1                                
)                            
AND (@CategoryType IS NULL OR Category.CategoryType = @CategoryType )                        
AND (@UniversityId IS NULL OR U.Id = @UniversityId )                        
AND (@CategoryId IS NULL OR Category.Id = @CategoryId )     
AND (@StudeyType IS NULL OR C.Studymethod = @StudeyType)      
AND (@State IS NULL OR U.State = @State)     
AND (@ZipCode IS NULL OR U.ZipCode like '%'+@ZipCode+'%')      
AND (@CourseLevel IS NULL OR C.CourseLevel = @CourseLevel)     
AND (@QualificationType IS NULL OR c.TrendingCourse = @QualificationType)     
order by C.CreatedDate desc;                           
else                                          
select              
C.Id,UniversityId,U.Legal_Name_Organisation as UniversityName,C.RTO_ID,C.CourseName,CourseProvider,AboutCourse,StudyMethod,                                  
CourseCategory, CategoryValue as CategoryName,CoursePhoto,                                    
CourseVideo,CourseStartDate,CourseDuration,CourseOverview,CourseOutcomes,                                    
CourseLearn,CourseRequirements,                                    
WorkPlacement,RPL,CourseGuide,C.CreatedDate,C.IsActive,Category.CategoryType,isnull(C.isReject,0) as IsReject,C.RejectionReason  , TD.CourseName 'TrendingCoursesName'  ,CL.CourseLevel 'CourseLevelName'                                   
,U.Logo as UniversityLogo    
from tbl_CourseDetails C with(nolock)                                   
inner join tbl_university_details U with(nolock) on C.UniversityId=U.Id                          
left join tbl_category_master Category with(nolock) on C.CourseCategory=Category.Id                 
left join tbl_TrendingCourses_Master TD with(nolock) on C.TrendingCourse=TD.Id                
left join  tbl_CourseLevel_Master CL with(nolock) on C.CourseLevel=CL.Id                
where                       
( @CategoryType IS NULL OR Category.CategoryType = @CategoryType )                        
AND ( @CategoryId IS NULL OR Category.Id = @CategoryId )                    
AND ( @UniversityId IS NULL OR U.Id = @UniversityId )       
AND (@StudeyType IS NULL OR C.Studymethod = @StudeyType)      
AND (@State IS NULL OR U.State = @State)     
AND (@ZipCode IS NULL OR U.ZipCode like '%'+@ZipCode+'%')      
AND (@CourseLevel IS NULL OR C.CourseLevel = @CourseLevel)     
AND (@QualificationType IS NULL OR c.TrendingCourse = @QualificationType)     
order by C.CreatedDate desc;                           
       
end 

create proc [dbo].[usp_get_courselevel]
as
begin
select * from tbl_CourseLevel_Master where IsActive=1
end

CREATE procedure [dbo].[usp_get_dashboard_count]  
as begin  
SELECT   
    COUNT(*) AS UTotal,  
    SUM(CASE WHEN IsActive = 0 THEN 1 ELSE 0 END) AS UNew,  
    SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS UVerified  
FROM tbl_university_details;  
  
SELECT   
    COUNT(*) AS CTotal,  
    SUM(CASE WHEN IsActive = 0 THEN 1 ELSE 0 END) AS CNew,  
    SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS CVerified  
FROM tbl_CourseDetails;  
  
SELECT   
    COUNT(*) AS TTotal,  
    SUM(CASE WHEN IsActive = 0 THEN 1 ELSE 0 END) AS TNew,  
    SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS TVerified  
FROM tbl_CourseTestimonial;  

SELECT   
    COUNT(*) AS LTotal 
FROM tbl_Lead;  
end

CREATE proc [dbo].[usp_get_Lead_Details]   --1 ,'P'  
(      
@id int,      
@LeadType varchar(10)      
)      
as begin      
if(@LeadType = 'P')      
select U.Legal_Name_Organisation as University_Name,S.Firstname+' '+S.Middlename+' '+S.lastname as Student_Name,    
U.General_Contact_Email as University_Email,C.CourseName as Course_Name, S.Mobile as Student_Mobile,  
S.isAustraliaResident as Student_isAustraliaResident, S.Zipcode as Student_Zipcode,S.State as Student_State,   
S.Country as Student_Country,S.EmailId as Student_EmailId ,PL.Student_ID as Student_ID
from tbl_Premium_Lead PL with(nolock)    
inner join tbl_CourseDetails C with(nolock) on C.Id=PL.Course_ID    
inner join tbl_student_details S with(nolock) on S.Id=PL.Student_ID    
inner join tbl_university_details U with(nolock) on U.Id=PL.University_ID    
where PL.id=@id      
else      
select U.Legal_Name_Organisation as University_Name,S.Firstname+' '+S.Middlename+' '+S.lastname as Student_Name,    
U.General_Contact_Email as University_Email,C.CourseName as Course_Name, S.Mobile as Student_Mobile,  
S.isAustraliaResident as Student_isAustraliaResident, S.Zipcode as Student_Zipcode,S.State as Student_State,   
S.Country as Student_Country,S.EmailId as Student_EmailId    
from tbl_Lead L with(nolock)     
inner join tbl_CourseDetails C with(nolock) on C.Id=L.Course_ID    
inner join tbl_student_details S with(nolock) on S.Id=L.Student_ID    
inner join tbl_university_details U with(nolock) on U.Id=L.University_ID    
where L.id=@id      
end

 CREATE proc [dbo].[usp_get_LeadsForUniversity] --6
 (
 @UniversityId int
 )
 as
 begin
select a.Course_ID,
a.Student_ID,
c.CourseName 'Course_Name',
d.CategoryValue as 'CourseCategory',
b.FirstName +' ' + b.MiddleName +' '+ b.LastName as 'Student_Name',
b.EmailId 'Student_EmailId',
b.Mobile 'Student_Mobile',
'Premium' 'LeadType'
from tbl_Premium_Lead(nolock) a
left join tbl_student_details(nolock) b on a.Student_ID=b.Id
left join tbl_CourseDetails(nolock) c on a.Course_ID=c.Id
left join tbl_category_master(nolock) d on c.CourseCategory=d.Id
where a.University_ID=@UniversityId and isnull(a.isEmailSent,0)=1
union all

select a.Course_ID,
a.Student_ID,
c.CourseName 'Course_Name',
d.CategoryValue as 'CourseCategory',
b.FirstName +' ' + b.MiddleName +' '+ b.LastName as 'Student_Name',
b.EmailId 'Student_EmailId',
b.Mobile 'Student_Mobile',
'Normal' 'LeadType'
from tbl_Lead(nolock) a
left join tbl_student_details(nolock) b on a.Student_ID=b.Id
left join tbl_CourseDetails(nolock) c on a.Course_ID=c.Id
left join tbl_category_master(nolock) d on c.CourseCategory=d.Id
where a.University_ID=@UniversityId and isnull(a.isEmailSent,0)=1
end

CREATE procedure [dbo].[usp_get_Masters]    
as begin    
select distinct Studymethod as objStudymethod  from tbl_CourseDetails as StudyMethod    
    
select distinct State as objState from tbl_university_details as State    
    
SELECT distinct LEFT(value, CHARINDEX('-', value) - 1) AS objZipCode FROM tbl_university_details CROSS APPLY STRING_SPLIT(ZipCode, ',') as ZipCode  
    
select Id as objCourseLevelId, CourseLevel as objCourseLevel from tbl_CourseLevel_Master as CourseLevel    
    
select Id as objQualificationLevelId, QualificationType as objQualificationLevel from tbl_QualificationLevel_master as QualificationType    
end

CREATE procedure [dbo].[usp_get_NormalLead]            
@Student_ID int =null                  
as begin                    
SET NOCOUNT ON;                  
                  
select  distinct            
L.Id,isnull(L.isEmailSent,0) as isEmailSent,L.EmailSentOn,L.createddate,                    
                  
C.ID as Course_ID,C.CourseName as Course_Name,C.CourseProvider,C.StudyMethod,C.CourseDuration,                  
Cate.CategoryValue as CourseCategory,                  
                  
U.ID as University_ID,U.Legal_Name_Organisation as University_Name,U.Email as University_Email,                    
U.Full_Name as University_Full_Name,U.Position as University_Position,U.Direct_Phone as University_Direct_Phone,                  
                  
S.Id as Student_ID,S.FirstName+ ' '+S.MiddleName+' '+S.LastName as Student_Name,S.EmailId as Student_EmailId,                    
S.Mobile as Student_Mobile,S.Zipcode as Student_Zipcode,S.State as Student_State,                  
S.Country as Student_Country,S.isAustraliaResident  as Student_isAustraliaResident                   
              
--select *                
from tbl_Lead L with(nolock)                    
inner join tbl_CourseDetails C with(nolock) on L.Course_ID=C.ID                    
inner join tbl_university_details U with(nolock) on U.ID=L.University_ID  and U.isUniversityActive=1             
inner join tbl_student_details S with(nolock) on L.Student_ID=S.ID                    
inner join tbl_category_master Cate with(nolock) on C.CourseCategory=Cate.Id       
inner join tbl_Premium_Lead PL with(nolock) on L.University_ID<>PL.University_ID --L.Course_ID<>PL.Course_ID --and isnull(L.Student_ID,0)<>isnull(PL.Student_ID,0) --and L.University_ID<>PL.University_ID    
    
    
                  
where (@Student_ID IS NULL OR L.Student_ID = @Student_ID) --and L.Course_ID<>PL.Course_ID and L.Student_ID<>PL.Student_ID and L.University_ID<>PL.University_ID;                
order by L.createddate   
end 

CREATE procedure [dbo].[usp_get_NormalLead_SendMail]              
@Student_ID int =null                    
as begin                      
SET NOCOUNT ON;                    
                    
select  distinct              
L.Id,isnull(L.isEmailSent,0) as isEmailSent,L.EmailSentOn,L.createddate,                      
                    
C.ID as Course_ID,C.CourseName as Course_Name,C.CourseProvider,C.StudyMethod,C.CourseDuration,                    
Cate.CategoryValue as CourseCategory,                    
                    
U.ID as University_ID,U.Legal_Name_Organisation as University_Name,U.Email as University_Email,                      
U.Full_Name as University_Full_Name,U.Position as University_Position,U.Direct_Phone as University_Direct_Phone,                    
                    
S.Id as Student_ID,S.FirstName+ ' '+S.MiddleName+' '+S.LastName as Student_Name,S.EmailId as Student_EmailId,                      
S.Mobile as Student_Mobile,S.Zipcode as Student_Zipcode,S.State as Student_State,                    
S.Country as Student_Country,S.isAustraliaResident  as Student_isAustraliaResident                     
                
--select *                  
from tbl_Lead L with(nolock)                      
inner join tbl_CourseDetails C with(nolock) on L.Course_ID=C.ID                      
inner join tbl_university_details U with(nolock) on U.ID=L.University_ID  and U.isUniversityActive=1               
inner join tbl_student_details S with(nolock) on L.Student_ID=S.ID                      
inner join tbl_category_master Cate with(nolock) on C.CourseCategory=Cate.Id         
inner join tbl_Premium_Lead PL with(nolock) on L.University_ID<>PL.University_ID --L.Course_ID<>PL.Course_ID --and L.Student_ID<>PL.Student_ID --and L.University_ID<>PL.University_ID      
      
      
                    
where (@Student_ID IS NULL OR L.Student_ID = @Student_ID)  and isnull(L.isEmailSent,0)=0 --and L.Course_ID<>PL.Course_ID and L.Student_ID<>PL.Student_ID and L.University_ID<>PL.University_ID;                  
order by L.createddate     
end 

CREATE procedure [dbo].[usp_get_Premium_Lead]  --5              
@Student_ID int =null              
as begin                
SET NOCOUNT ON;              
              
select                 
L.Id,isnull(L.isEmailSent,0) as isEmailSent,L.EmailSentOn,L.createddate,                
              
C.ID as Course_ID,C.CourseName as Course_Name,C.CourseProvider,C.StudyMethod,C.CourseDuration,              
Cate.CategoryValue as CourseCategory,              
              
U.ID as University_ID,U.Legal_Name_Organisation as University_Name,U.Email as University_Email,                
U.Full_Name as University_Full_Name,U.Position as University_Position,U.Direct_Phone as University_Direct_Phone,              
              
S.Id as Student_ID,S.FirstName+ ' '+S.MiddleName+' '+S.LastName as Student_Name,S.EmailId as Student_EmailId,                
S.Mobile as Student_Mobile,S.Zipcode as Student_Zipcode,S.State as Student_State,              
S.Country as Student_Country,S.isAustraliaResident  as Student_isAustraliaResident               
              
from tbl_Premium_Lead L with(nolock)                
inner join tbl_CourseDetails C with(nolock) on L.Course_ID=C.ID                
inner join tbl_university_details U with(nolock) on C.UniversityId=U.ID and U.isUniversityActive=1               
inner join tbl_student_details S with(nolock) on L.Student_ID=S.ID                
inner join tbl_category_master Cate with(nolock) on C.CourseCategory=Cate.Id              
              
where (@Student_ID IS NULL OR L.Student_ID = @Student_ID)
order by L.createddate desc;              
                
end 

CREATE procedure [dbo].[usp_get_Premium_Lead_SendMail]  --5                
@Student_ID int =null                
as begin                  
SET NOCOUNT ON;                
                
select                   
L.Id,isnull(L.isEmailSent,0) as isEmailSent,L.EmailSentOn,L.createddate,                  
                
C.ID as Course_ID,C.CourseName as Course_Name,C.CourseProvider,C.StudyMethod,C.CourseDuration,                
Cate.CategoryValue as CourseCategory,                
                
U.ID as University_ID,U.Legal_Name_Organisation as University_Name,U.Email as University_Email,                  
U.Full_Name as University_Full_Name,U.Position as University_Position,U.Direct_Phone as University_Direct_Phone,                
                
S.Id as Student_ID,S.FirstName+ ' '+S.MiddleName+' '+S.LastName as Student_Name,S.EmailId as Student_EmailId,                  
S.Mobile as Student_Mobile,S.Zipcode as Student_Zipcode,S.State as Student_State,                
S.Country as Student_Country,S.isAustraliaResident  as Student_isAustraliaResident                 
                
from tbl_Premium_Lead L with(nolock)                  
inner join tbl_CourseDetails C with(nolock) on L.Course_ID=C.ID                  
inner join tbl_university_details U with(nolock) on C.UniversityId=U.ID and U.isUniversityActive=1                 
inner join tbl_student_details S with(nolock) on L.Student_ID=S.ID                  
inner join tbl_category_master Cate with(nolock) on C.CourseCategory=Cate.Id                
                
where (@Student_ID IS NULL OR L.Student_ID = @Student_ID)  and isnull(L.isEmailSent,0)=0
order by L.createddate desc;                
                  
end 

CREATE procedure [dbo].[usp_get_search_course_details]  --'kot'          
(            
@Keyword varchar(500)           
)            
as begin            
 select       
 C.Id,UniversityId,U.Trading_Name as UniversityName,C.RTO_ID,CourseName,CourseProvider,AboutCourse,StudyMethod,    
 CourseCategory, CategoryValue as CategoryName,CoursePhoto,      
 CourseVideo,CourseStartDate,CourseDuration,CourseOverview,CourseOutcomes,      
 CourseLearn,CourseRequirements,      
 WorkPlacement,RPL,CourseGuide,C.CreatedDate,C.IsActive      
 from tbl_CourseDetails C with(nolock)     
 left join tbl_university_details U with(nolock) on C.UniversityId=U.Id    
 left join tbl_category_master Category with(nolock) on C.CourseCategory=Category.Id    
 where C.isactive=1     
 and     
 (    
 C.CourseName like '%'+@Keyword+'%' or CourseProvider like '%'+@Keyword+'%' or    
 U.Trading_Name like '%'+@Keyword+'%' or Category.CategoryValue like '%'+@Keyword+'%'    
 )        
end        
      
      

      CREATE procedure [dbo].[usp_get_student]  
(  
@Id int  
)  
as begin  
 if(@Id=0)  
 select * from tbl_student_details nolock  order by CreateDate desc
 else  
 select * from tbl_student_details nolock where Id=@Id  order by CreateDate desc
  
end  

CREATE procedure [dbo].[usp_get_testimonial]                 
(                
@Testimonial_Id int,         
@Course_Id int,        
@Student_Id int,        
@LoginType varchar(50)                
)                
as begin                    
if((@LoginType='Admin' or @LoginType='University') and @Testimonial_Id=0)                
begin         
select T.Id,T.Course_Id,T.Person_Name,T.Designation,T.Comment,T.IsActive,T.CreatedDate,C.CourseName,U.Trading_Name                  
from tbl_CourseTestimonial T with(nolock)                  
left join tbl_CourseDetails C with(nolock) on C.Id=T.Course_Id                  
left join tbl_university_details U with(nolock) on C.UniversityId=U.Id     
order by T.CreatedDate desc
end                
else if(@LoginType='Student' and @Testimonial_Id=0)                
begin         
select T.Id,T.Course_Id,T.Person_Name,T.Designation,T.Comment,T.IsActive,T.CreatedDate,C.CourseName,U.Trading_Name                  
from tbl_CourseTestimonial T with(nolock)                  
left join tbl_CourseDetails C with(nolock) on C.Id=T.Course_Id                  
left join tbl_university_details U with(nolock) on C.UniversityId=U.Id           
where T.Student_Id=@Student_Id      
order by T.CreatedDate desc
end                
else if(@Course_Id<>0)                
begin         
select T.Id,T.Course_Id,T.Person_Name,T.Designation,T.Comment,T.IsActive,T.CreatedDate,C.CourseName,U.Trading_Name                  
from tbl_CourseTestimonial T with(nolock)                  
left join tbl_CourseDetails C with(nolock) on C.Id=T.Course_Id                  
left join tbl_university_details U with(nolock) on C.UniversityId=U.Id        
where  T.Course_Id=@Course_Id and T.IsActive=1   
order by T.CreatedDate desc
end                
else                
select T.Id,Course_Id,Person_Name,Designation,Comment,T.IsActive,T.CreatedDate,C.CourseName,U.Trading_Name                  
from tbl_CourseTestimonial T with(nolock)                  
left join tbl_CourseDetails C with(nolock) on C.Id=T.Course_Id                  
left join tbl_university_details U with(nolock) on C.UniversityId=U.Id                  
where  T.ID=@Testimonial_Id  
order by T.CreatedDate desc
end 

create proc [dbo].[usp_get_trendingcourses]
as
begin
select * from tbl_TrendingCourses_Master where IsActive=1
end

CREATE proc [dbo].[usp_get_UniversistyDashboardCounts] --6
(@UniversityId int
)
as
begin
select (select Count(*) EnrollCourses from tbl_CourseDetails(nolock) where UniversityId=@UniversityId ) EnrollCourses,
    (select Count(*) ActiveCourses from tbl_CourseDetails(nolock) where UniversityId=@UniversityId and isnull(IsActive,0)=1)  ActiveCourses,
	(select Count(*) NormalLead from tbl_Lead(nolock) where University_ID=@UniversityId and isnull(isEmailSent,0)=0) NormalLead,
(select Count(*) PremiumLead from tbl_Premium_Lead(nolock) where University_ID=@UniversityId and isnull(isEmailSent,0)=1) PremiumLead
end

CREATE procedure [dbo].[usp_get_university]                    
(                    
@Id int                    
)                    
as begin                    
 if(@Id=0)                    
 select                 
 U.Id,RTO_ID,RTO_Status,CRICOS_Code,Legal_Name_Organisation,Trading_Name,ABN_ACN,Business_Address,                
 Postal_Address,Website,Contact_Number,General_Contact_Email,Full_Name,Position,Direct_Phone,Email,                
 --Relevant_Courses,            
 Signature,                
 Logo,Password,U.IsActive,U.CreateDate,U.IsReject,U.RejectionReason ,isUniversityActive               
 from tbl_university_details U with(nolock)     --where IsActive=1    
 order by U.CreateDate desc
 else                    
 select  U.Id,RTO_ID,RTO_Status,CRICOS_Code,Legal_Name_Organisation,Trading_Name,ABN_ACN,Business_Address,                
 Postal_Address,Website,Contact_Number,General_Contact_Email,Full_Name,Position,Direct_Phone,Email,                
 --Relevant_Courses,            
 Signature,                
 Logo,Password,U.IsActive,U.CreateDate,U.IsReject,U.RejectionReason,isUniversityActive         
 from tbl_university_details U with(nolock) where Id=@Id    -- and IsActive=1         
 order by U.CreateDate desc
 
end

CREATE proc [dbo].[usp_GetBlogDetails]
as
begin
select * from tbl_Blogs where BlogVerified=1
end


CREATE proc [dbo].[usp_getBlogs]      
(      
@id int=null,      
@Type varchar(200)='Admin' ,
@slug varchar(max)=null
)      
as      
begin      
      
      
if(@id is null and @Type='Admin')      
begin      
select id    
,BlogTitle    
,OnelinerDiscription    
,Quote    
,FullDiscription    
,SubTitle    
,SubTitleDiscription    
,BlogThumbnail    
,BlogSmallThumbnail1    
,BlogSmallThumbnail2    
,BlogSmallThumbnail3    
,BlogVerified    
,format(a.CreatedDate,'dd-MM-yyyy') CreatedDate     
,b.name as 'CreatedBy'    
,ModifyDate    
,ModifyBy     
from tbl_Blogs a     
left join tbl_UserRegistration b on Convert(int,a.CreatedBy) =  b.userId order by CreatedDate,ModifyDate      
end      
if(@id is not null and @Type='Admin')      
begin      
select id    
,BlogTitle    
,OnelinerDiscription    
,Quote    
,FullDiscription    
,SubTitle    
,SubTitleDiscription    
,BlogThumbnail    
,BlogSmallThumbnail1    
,BlogSmallThumbnail2    
,BlogSmallThumbnail3    
,BlogVerified    
,format(a.CreatedDate,'dd-MM-yyyy') CreatedDate     
,b.name as 'CreatedBy'    
,ModifyDate    
,ModifyBy     
from tbl_Blogs a     
left join tbl_UserRegistration b on Convert(int,a.CreatedBy) =  b.userId where Id=@id order by a.CreatedDate,ModifyDate      
end      
else if(@Type='User')      
begin      
select id    
,BlogTitle    
,OnelinerDiscription    
,Quote    
,FullDiscription    
,SubTitle    
,SubTitleDiscription    
,BlogThumbnail    
,BlogSmallThumbnail1    
,BlogSmallThumbnail2    
,BlogSmallThumbnail3    
,BlogVerified    
,format(a.CreatedDate,'dd-MM-yyyy') CreatedDate     
,b.name as 'CreatedBy'    
,ModifyDate    
,ModifyBy     
from tbl_Blogs a     
left join tbl_UserRegistration b on Convert(int,a.CreatedBy) =  b.userId 
where (Id=@id or BlogTitle=@slug) and BlogVerified=1 order by a.CreatedDate,ModifyDate      
end      
      
end  

create proc [dbo].[usp_GetCategoriesCount]
as
begin
select top 8 Category.Id ,CategoryValue,Category.ImagePath,Count(*) Count
from tbl_CourseDetails a
left join tbl_category_master Category with(nolock) on a.CourseCategory=Category.Id 
group by CategoryValue,Category.Id ,Category.ImagePath
order by Count(*)  desc
end

create proc [dbo].[usp_GetCourseCount]
as
begin
SELECT 
    CASE 
        WHEN COUNT(*) = 0 THEN 0 
        ELSE COUNT(*) 
    END AS Cnt
FROM tbl_CourseDetails
WHERE IsActive = 0;
end

CREATE proc [dbo].[usp_GetDonutData]  
as begin  
select 1 as isCountry,country,count(country)as count,'#' + RIGHT('000000' + CONVERT(varchar(6), CAST(NEWID() AS varbinary(4)), 2), 6) AS ColorCode from (
select  case when isAustraliaResident=1 then 'Australia' else country end country from tbl_student_details  
where country is not null ) tbl--isnull(country,'0') <> '0'  
group by country
  
union   
select 0 as isCountry,state,count(state) as count, '#' + RIGHT('000000' + CONVERT(varchar(6), CAST(NEWID() AS varbinary(4)), 2), 6) AS ColorCode from tbl_student_details  
where state is not null and state<>''  
group by state  
end  

CREATE proc [dbo].[usp_GetPieData]  
as begin  
select C.CategoryType as CategoryType,count(CourseCategory)as count,'#' + RIGHT('000000' + CONVERT(varchar(6), CAST(NEWID() AS varbinary(4)), 2), 6) AS ColorCode 
from tbl_CourseDetails Course  
inner join tbl_category_master C on Course.CourseCategory=C.id  
group by CategoryType  
end  
  
  
  CREATE procedure [dbo].[usp_GetProfile]          
(          
@Id int,          
@LoginType varchar(50)          
)          
as begin          
if(@LoginType='Student')          
begin          
select * from tbl_student_details nolock where id=@Id          
end          
else          
begin          
SELECT         
U.Id, U.RTO_ID, U.RTO_Status, U.CRICOS_Code,        
U.Legal_Name_Organisation, U.Trading_Name, U.ABN_ACN,        
U.Business_Address, U.Postal_Address, U.Website,        
U.Contact_Number, U.General_Contact_Email,        
U.Full_Name, U.Position, U.Direct_Phone, U.Email,        
        
STRING_AGG(C.CategoryValue, ', ') AS Relevant_Courses_String,        
        
U.Signature, U.Logo, U.[Password], U.IsActive, U.CreateDate,      
U.IsReject,U.RejectionReason,U.isUniversityActive,ZipCode,State     
FROM tbl_university_details U WITH (NOLOCK)        
        
CROSS APPLY STRING_SPLIT(U.Relevant_Courses, ',') AS S        
INNER JOIN tbl_category_master C WITH (NOLOCK)         
ON C.Id = CAST(S.value AS INT)        
where U.id=@Id        
GROUP BY         
U.Id, U.RTO_ID, U.RTO_Status, U.CRICOS_Code,        
U.Legal_Name_Organisation, U.Trading_Name, U.ABN_ACN,        
U.Business_Address, U.Postal_Address, U.Website,        
U.Contact_Number, U.General_Contact_Email,        
U.Full_Name, U.Position, U.Direct_Phone, U.Email,        
U.Signature, U.Logo, U.[Password], U.IsActive, U.CreateDate,        
U.IsReject,U.RejectionReason,U.isUniversityActive,ZipCode,State              
end          
end 

create proc [dbo].[usp_GetUniversityCount]
as
begin
select case when count(*)=0 then 0 else count(*) end Cnt from tbl_University_details where IsActive=0
end

CREATE proc [dbo].[usp_LostPassword]
(
@EmailId varchar(500),
@Password varchar(500)
)
as
begin

if exists(select * from tbl_Student_Details where IsActive=1 and EmailId=@EmailId)
begin
update tbl_Student_Details
set Password=@Password
where EmailId=@EmailId

select 'Success!! Your new password has been sent to your registered email address. Please check your inbox and log in using the new password.' Message
end
else if exists(select * from tbl_university_details where IsActive=1 and Email=@EmailId)
begin
update tbl_university_details
set Password=@Password
where Email=@EmailId
select 'Success!! Your new password has been sent to your registered email address. Please check your inbox and log in using the new password.' Message

end
else
begin
select 'Failed!! Email address not found in our system. Please enter a valid registered email address to recover your password.' Message
end

end

CREATE procedure [dbo].[usp_monthwise_lead] --4,0    
(    
@UniversityId int=null 
)      
as begin      

DECLARE @Year INT = YEAR(GETDATE());

SELECT 
    DATENAME(MONTH, DATEFROMPARTS(@Year, m.MonthNo, 1)) AS MonthName,
    ISNULL(p.PremiumLead, 0) AS PremiumLead,
    ISNULL(n.NormalLead, 0) AS NormalLead
FROM
(
    SELECT 1 AS MonthNo UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL
    SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL
    SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9 UNION ALL
    SELECT 10 UNION ALL SELECT 11 UNION ALL SELECT 12
) m
LEFT JOIN
(
    SELECT 
        MONTH(Createddate) AS MonthNo,
        COUNT(*) AS PremiumLead
    FROM tbl_Premium_Lead
    WHERE YEAR(Createddate) = @Year
      AND (@UniversityId IS NULL OR University_ID = @UniversityId)
    GROUP BY MONTH(Createddate)
) p ON m.MonthNo = p.MonthNo
LEFT JOIN
(
    SELECT 
        MONTH(Createddate) AS MonthNo,
        COUNT(*) AS NormalLead
    FROM tbl_Lead
    WHERE YEAR(Createddate) = @Year
      AND (@UniversityId IS NULL OR University_ID = @UniversityId)
    GROUP BY MONTH(Createddate)
) n ON m.MonthNo = n.MonthNo
ORDER BY m.MonthNo;


end 



CREATE proc [dbo].[usp_reject_Course]
(
@CourseId int,
@Reason varchar(max)
)
as begin
update	tbl_coursedetails
set Isactive=0, IsReject=1,RejectionReason=@Reason
where Id=@CourseId
select 'Rejected' as msg
end

CREATE procedure [dbo].[usp_retrive_ArchiveUniversity]   
(@UniversityId int)
as begin  
insert into tbl_university_details (RTO_ID,RTO_Status,CRICOS_Code,Legal_Name_Organisation,Trading_Name,ABN_ACN,Business_Address,                  
 Postal_Address,Website,Contact_Number,General_Contact_Email,Full_Name,Position,Direct_Phone,Email,                  
 Relevant_Courses,              
 Signature,                  
 Logo,Password,U.IsActive,U.CreateDate,U.IsReject,U.RejectionReason ,isUniversityActive  ) 
 select                   
 RTO_ID,RTO_Status,CRICOS_Code,Legal_Name_Organisation,Trading_Name,ABN_ACN,Business_Address,                  
 Postal_Address,Website,Contact_Number,General_Contact_Email,Full_Name,Position,Direct_Phone,Email,                  
 Relevant_Courses,              
 Signature,                  
 Logo,Password,U.IsActive,U.CreateDate,U.IsReject,U.RejectionReason ,isUniversityActive                 
 from tbl_university_details_archive U with(nolock)     --where IsActive=1      
 WHERE U.Id = @UniversityId
 select 'Retrive' as msg
end

CREATE procedure [dbo].[usp_staus_change_testimonial]    
(    
@Testimonail_Id int,
@IsActive int
)    
as begin    
update tbl_CourseTestimonial   
set IsActive=@IsActive where Id=  @Testimonail_Id  

 select case when IsActive=0 then 'Inactived' else 'Actived' end as msg         
 from tbl_CourseTestimonial with(nolock) where Id = @Testimonail_Id  
end

CREATE proc [dbo].[usp_UpdateLeadAfterEmail]   --1,'P'  
(    
@id int,    
@LeadType varchar(10)    
)    
as begin    
if(@LeadType = 'P')    
begin  
update tbl_Premium_Lead set isEmailSent=1,EmailSentOn=getdate() where id=@id   
  
end  
else    
begin  
update tbl_Lead  set isEmailSent=1,EmailSentOn=getdate() where id=@id    
end  
select 1   
end

CREATE proc [dbo].[usp_UserLogin] --'ravighiware9@gmail.com','Sanket@123',0        
(          
@EmailId varchar(100),          
@Password varchar(100),        
@IsStudent int        
)          
as          
begin          
if(@IsStudent=1)        
 begin         
  if exists(select * from tbl_student_details(nolock) where EmailId=@EmailId and Password=@Password and IsActive=1)          
   begin          
    select * from tbl_student_details(nolock) where EmailId=@EmailId and Password=@Password and IsActive=1          
   end          
  else if exists(select * from tbl_student_details(nolock) where EmailId=@EmailId and IsActive=1)          
   begin          
    select 'Invalid Password' as SqlMsg          
   end          
  else if exists(select * from tbl_student_details(nolock) where Password=@Password and IsActive=1)          
   begin          
    select 'Invalid UserId' as SqlMsg          
   end        
  else          
   begin          
    select 'User not available in system, Please contact admin and create your account.' as SqlMsg          
   end          
 end        
else        
 begin        
 if exists(select * from tbl_university_details(nolock) where General_Contact_Email=@EmailId and Password=@Password and IsActive=1 and isUniversityActive=1)          
  begin          
   select         
   Id,RTO_ID,RTO_Status,CRICOS_Code,Legal_Name_Organisation,Trading_Name,ABN_ACN,Business_Address,        
   Postal_Address,Website,Contact_Number,General_Contact_Email,Full_Name,Position,         
   Direct_Phone,Email,--Relevant_Courses,        
   Signature,Logo,Password,IsActive,CreateDate  
   from tbl_university_details(nolock) where General_Contact_Email=@EmailId and Password=@Password and IsActive=1 and isUniversityActive=1         
   --Id,RTO_ID as ,Legal_Name_Organisation 'Name',Email 'EmailId' ,Contact_Number 'Mobile' , 'University' as 'User','' 'Message'    
     
     
  
  end          
 else if exists(select * from tbl_university_details(nolock) where General_Contact_Email=@EmailId and isUniversityActive=1)          
  begin          
   select 'Invalid Password' as SqlMsg        
  end          
 else if exists(select * from tbl_university_details(nolock) where Password=@Password and IsActive=1 and isUniversityActive=1)          
  begin          
   select 'Invalid UserId' as SqlMsg        
  end          
 else          
  begin          
   select 'User not available in system, Please contact admin and create your account.' as SqlMsg        
  end         
 end        
end   

CREATE procedure [dbo].[usp_verify_student_email]  
@link varchar(1000),  
@emailid varchar(500)  
as begin  
if exists(select 1 from tbl_student_details nolock where EmailId=@EmailId and VerifyLink=@link and isActive=1)  
begin  
Select 'Already Verified' as msg  
end  
else if exists(select 1 from tbl_student_details nolock where EmailId=@EmailId and VerifyLink=@link and isActive=0)  
begin  
update tbl_student_details set isActive=1 where EmailId=@EmailId and VerifyLink=@link and isActive=0  
Select 'Verification Successfull'+','+Firstname+' '+ Middlename+' '+ Lastname as msg   from tbl_student_details nolock where EmailId=@EmailId and VerifyLink=@link and isActive=1 
end  
else  
begin  
Select 'Invalid Link' as msg  
end  
end