@echo off
echo ========================================
echo    CRIADOR DE EXECUTAVEL UNICO
echo ========================================
echo.

echo Criando executavel unico (self-contained)...
dotnet publish painelff.sln -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo    EXECUTAVEL UNICO CRIADO!
    echo ========================================
    echo.
    echo O executavel unico foi criado em:
    echo painelff\bin\Release\net8.0-windows\win-x64\publish\painelff.exe
    echo.
    echo VANTAGENS DO EXECUTAVEL UNICO:
    echo - Nao precisa do .NET Runtime instalado
    echo - Arquivo unico facil de distribuir
    echo - Funciona em qualquer Windows 10/11 64-bit
    echo.
    echo TAMANHO APROXIMADO: ~50-100 MB
    echo.
    echo Para distribuir, envie apenas o arquivo:
    echo painelff.exe
    echo.
) else (
    echo.
    echo ========================================
    echo    ERRO NA CRIACAO!
    echo ========================================
    echo.
    echo Verifique se voce tem o .NET 8.0 SDK instalado
    echo e se todos os arquivos do projeto estao presentes.
    echo.
)

pause 