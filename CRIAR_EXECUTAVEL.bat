@echo off
echo ========================================
echo    CRIADOR DE EXECUTAVEL - PAINEL FF
echo ========================================
echo.

echo Compilando projeto em modo Release...
dotnet build painelff.sln --configuration Release

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo    COMPILACAO CONCLUIDA COM SUCESSO!
    echo ========================================
    echo.
    echo O executavel foi criado em:
    echo painelff\bin\Release\net8.0-windows\painelff.exe
    echo.
    echo Para distribuir, copie TODA a pasta:
    echo painelff\bin\Release\net8.0-windows\
    echo.
    echo Arquivos necessarios para distribuir:
    echo - painelff.exe (executavel principal)
    echo - painelff.dll (biblioteca principal)
    echo - painelff.deps.json (dependencias)
    echo - painelff.runtimeconfig.json (configuracao)
    echo - Memory.dll (biblioteca de memoria)
    echo - Memory.xml (configuracao da memoria)
    echo - Guna.UI2.dll (interface grafica)
    echo - Newtonsoft.Json.dll (JSON)
    echo - System.Management.dll (gerenciamento)
    echo - runtimes\ (pasta com runtimes)
    echo.
    echo IMPORTANTE: Execute sempre como ADMINISTRADOR!
    echo.
) else (
    echo.
    echo ========================================
    echo    ERRO NA COMPILACAO!
    echo ========================================
    echo.
    echo Verifique se voce tem o .NET 8.0 SDK instalado
    echo e se todos os arquivos do projeto estao presentes.
    echo.
)

pause 