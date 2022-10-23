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

# overwrites the project file created previously
func init Bot --force

# figure out which templates are supported
func templates list

# adding http webhook
func new --name NominateController --template "HTTP trigger"


```

cBvcO3re2Gu7mVtgjcJkcJYC1FQrIVxeoUTO19mMV5w=
