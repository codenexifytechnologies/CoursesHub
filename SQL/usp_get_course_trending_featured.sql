/*
  Trending / Featured course (set by the admin in Admin > Edit Course Details)
    TrendingCourse (varchar): '1' = trending, '0' = not trending
    FeaturedCourse (varchar): 'High' / 'Medium' / 'Low', NULL = not featured

  Run the three steps in order.
*/

/* 1) Existing rows: anything that is not '1' becomes '0' (old Yes/No values are converted too). */
UPDATE tbl_CourseDetails
SET TrendingCourse = CASE WHEN LTRIM(RTRIM(ISNULL(TrendingCourse, ''))) IN ('1', 'Yes') THEN '1' ELSE '0' END;

UPDATE tbl_CourseDetails
SET FeaturedCourse = NULL
WHERE FeaturedCourse IS NOT NULL AND FeaturedCourse NOT IN ('High', 'Medium', 'Low');
GO

/* 2) Admin save: default '0' instead of 'No' (a 'No' value breaks the int join in usp_get_course). */
ALTER procedure [usp_change_status_course]
(
@Course_Id int,
@IsActive int,
@TrendingCourse varchar(10) = '0',
@FeaturedCourse varchar(10) = NULL
)
as begin

 update tbl_CourseDetails
 set isReject=0,RejectionReason='', IsActive=@IsActive,
  TrendingCourse = CASE WHEN @TrendingCourse IN ('1', 'Yes') THEN '1' ELSE '0' END,
  FeaturedCourse = CASE WHEN @FeaturedCourse IN ('High','Medium','Low') THEN @FeaturedCourse ELSE NULL END
 where Id=  @Course_Id

 update tbl_CourseTestimonial
 set IsActive=@IsActive where Course_Id=  @Course_Id

 select case when a.IsActive=0 then 'Inactivated' else 'Activated' end  +','+a.CourseName+','+ b.Legal_Name_Organisation+','+b.General_Contact_Email as msg
 from tbl_CourseDetails(nolock) a
 left join tbl_university_details(nolock) b on a.UniversityId=b.Id
 where a.Id = @Course_Id

end
GO

/* 3) usp_get_course: return FeaturedCourse and drop the old join to tbl_TrendingCourses_Master
      (TrendingCourse is no longer a master Id, so '1' would wrongly match master row Id 1).
      Everything else is the same as the live version. */
ALTER procedure [usp_get_course]  --0,'',null,null
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
CourseVideo,CourseVideo2,CourseVideo3,CourseStartDate,CourseDuration,CourseOverview,CourseOutcomes,
CourseLearn,CourseRequirements,
WorkPlacement,RPL,CourseGuide,C.CreatedDate,C.IsActive,Category.CategoryType,isnull(C.isReject,0) as IsReject,C.RejectionReason,
C.TrendingCourse 'TrendingCourses', C.FeaturedCourse, C.CourseLevel ,CL.CourseLevel 'CourseLevelName'
,isnull (U.Logo,'https://courseshub.com.au/Uploads/Logo/NoImage.jpg') as UniversityLogo   ,U.State, C.GovernmentFunding  ,C.StudentType
from tbl_CourseDetails C with(nolock)
inner join tbl_university_details U with(nolock) on C.UniversityId=U.Id
left join tbl_category_master Category with(nolock) on C.CourseCategory=Category.Id
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
CourseVideo,CourseVideo2,CourseVideo3,CourseStartDate,CourseDuration,CourseOverview,CourseOutcomes,
CourseLearn,CourseRequirements,
WorkPlacement,RPL,CourseGuide,C.CreatedDate,C.IsActive,Category.CategoryType,isnull(C.isReject,0) as IsReject,C.RejectionReason,
C.TrendingCourse 'TrendingCourses', C.FeaturedCourse, C.CourseLevel ,CL.CourseLevel 'CourseLevelName'
,isnull (U.Logo,'https://courseshub.com.au/Uploads/Logo/NoImage.jpg') as UniversityLogo   ,U.State ,C.GovernmentFunding,C.StudentType
from tbl_CourseDetails C with(nolock)
inner join tbl_university_details U with(nolock) on C.UniversityId=U.Id
left join tbl_category_master Category with(nolock) on C.CourseCategory=Category.Id
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
GO
