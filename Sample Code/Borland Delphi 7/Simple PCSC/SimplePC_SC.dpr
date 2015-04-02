program SimplePC_SC;

uses
  Forms,
  SimplePCSC in 'SimplePCSC.pas' {MainReadWrite};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainReadWrite, MainReadWrite);
  Application.Run;
end.
