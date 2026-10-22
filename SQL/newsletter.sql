/*
  Newsletter subscriptions (footer "Newsletter" form)
    - tbl_NewsletterSubscriber        one row per email address
    - usp_newsletter_subscribe        subscribe / re-subscribe an email; returns Result + Token
    - usp_newsletter_unsubscribe      unsubscribe by the token from the email link; returns Result + Email
    - usp_get_newsletter_subscribers  list for the admin panel
  Run once on the database (livedigi_courseshub).
*/

IF OBJECT_ID('tbl_NewsletterSubscriber', 'U') IS NULL
BEGIN
    CREATE TABLE tbl_NewsletterSubscriber
    (
        Id               int IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Email            nvarchar(256)     NOT NULL,
        Token            uniqueidentifier  NOT NULL DEFAULT NEWID(),
        IsActive         bit               NOT NULL DEFAULT 1,
        SubscribedDate   datetime          NOT NULL DEFAULT GETDATE(),
        UnsubscribedDate datetime          NULL,
        Source           nvarchar(100)     NULL
    );
    CREATE UNIQUE INDEX UX_NewsletterSubscriber_Email ON tbl_NewsletterSubscriber (Email);
    CREATE UNIQUE INDEX UX_NewsletterSubscriber_Token ON tbl_NewsletterSubscriber (Token);
END
GO

CREATE OR ALTER PROCEDURE usp_newsletter_subscribe
(
    @Email  nvarchar(256),
    @Source nvarchar(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @Email = LOWER(LTRIM(RTRIM(@Email)));

    IF EXISTS (SELECT 1 FROM tbl_NewsletterSubscriber WHERE Email = @Email AND IsActive = 1)
    BEGIN
        SELECT 'AlreadySubscribed' AS Result, Token FROM tbl_NewsletterSubscriber WHERE Email = @Email;
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM tbl_NewsletterSubscriber WHERE Email = @Email)
    BEGIN
        UPDATE tbl_NewsletterSubscriber
        SET IsActive = 1, SubscribedDate = GETDATE(), UnsubscribedDate = NULL, Source = ISNULL(@Source, Source)
        WHERE Email = @Email;

        SELECT 'Resubscribed' AS Result, Token FROM tbl_NewsletterSubscriber WHERE Email = @Email;
        RETURN;
    END

    INSERT INTO tbl_NewsletterSubscriber (Email, Source) VALUES (@Email, @Source);
    SELECT 'Subscribed' AS Result, Token FROM tbl_NewsletterSubscriber WHERE Email = @Email;
END
GO

CREATE OR ALTER PROCEDURE usp_newsletter_unsubscribe
(
    @Token uniqueidentifier
)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM tbl_NewsletterSubscriber WHERE Token = @Token)
    BEGIN
        SELECT 'NotFound' AS Result, CAST(NULL AS nvarchar(256)) AS Email;
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM tbl_NewsletterSubscriber WHERE Token = @Token AND IsActive = 0)
    BEGIN
        SELECT 'AlreadyUnsubscribed' AS Result, Email FROM tbl_NewsletterSubscriber WHERE Token = @Token;
        RETURN;
    END

    UPDATE tbl_NewsletterSubscriber SET IsActive = 0, UnsubscribedDate = GETDATE() WHERE Token = @Token;
    SELECT 'Unsubscribed' AS Result, Email FROM tbl_NewsletterSubscriber WHERE Token = @Token;
END
GO

CREATE OR ALTER PROCEDURE usp_get_newsletter_subscribers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Email, IsActive, SubscribedDate, UnsubscribedDate, Source
    FROM tbl_NewsletterSubscriber
    ORDER BY SubscribedDate DESC;
END
GO
