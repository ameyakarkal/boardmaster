# :mage: board master

randomly picks team members for an activity like "share standup board"

![overview](.attachments/boardmaster.drawio.png)

## :hammer_and_wrench: how i build this

```bash
#cwd : repo root
dotnet new gitignore

#cwd : src
dotnet new console Bot

#cwd : tests
dotnet new xunit Bot.Tests
```

adding azure function

```
# install azure function cli tools
# i had these pre installed follow these instructions

# overwrites the project file created previously
func init Bot --force

# adding http webhook
# figure out which templates are supported
func templates list

```

cBvcO3re2Gu7mVtgjcJkcJYC1FQrIVxeoUTO19mMV5w=
