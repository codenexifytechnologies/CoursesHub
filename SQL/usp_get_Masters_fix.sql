/*
  usp_get_Masters (header "Find your course" filter menu)

  Problem: the zip code query used LEFT(value, CHARINDEX('-', value) - 1). For a zip code value without a "-"
  CHARINDEX returns 0, LEFT gets -1 and SQL Server throws
      "Invalid length parameter passed to the LEFT or SUBSTRING function."
  The error stops the procedure after the State list, so Course Level and Qualification Level never reach the site
  and the "Courses By Qualification Level" filter menu is empty.

  Fix: only cut at "-" when there is one; also skip empty values. Everything else is unchanged.
*/
ALTER procedure usp_get_Masters
as begin
select distinct Studymethod as objStudymethod  from tbl_CourseDetails as StudyMethod

select distinct State as objState from tbl_university_details as State

SELECT distinct LTRIM(RTRIM(CASE WHEN CHARINDEX('-', value) > 0 THEN LEFT(value, CHARINDEX('-', value) - 1) ELSE value END)) AS objZipCode
FROM tbl_university_details CROSS APPLY STRING_SPLIT(ZipCode, ',') as ZipCode
WHERE LTRIM(RTRIM(value)) <> ''

select Id as objCourseLevelId, CourseLevel as objCourseLevel from tbl_CourseLevel_Master as CourseLevel

select Id as objQualificationLevelId, QualificationType as objQualificationLevel from tbl_QualificationLevel_master as QualificationType
end
GO
