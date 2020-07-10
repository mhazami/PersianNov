Go
alter table [Payment].TaskMoney add Amount decimal(18,3)
GO
alter table [payment].Wallet add BookId uniqueidentifier
GO


ALTER TABLE [Payment].[Wallet]  WITH CHECK ADD  CONSTRAINT [FK_Wallet_Book] FOREIGN KEY([BookId])
REFERENCES [Book].[Book] ([Id])
GO

ALTER TABLE [Payment].[Wallet] CHECK CONSTRAINT [FK_Wallet_Book]
GO