$Runner = "../lrs-conformance-test-suite/bin/console_runner.js";
$Url = "http://localhost:52209";
$User = "admin@example.com";
$Password = "zKR4gkYNHP5tvH";
$args = @(
    "$Runner",
    "-e $Url/xapi",
    "-u $User",
    "-p $Password",
    "--errors"
)
$p= Start-Process node -ArgumentList "lrs-conformance-test-suite/bin/console_runner.js -e http://localhost:52209/xapi -a -u admin@example.com -p zKR4gkYNHP5tvH --errors" -wait -NoNewWindow -PassThru
$p.HasExited
$p.ExitCode