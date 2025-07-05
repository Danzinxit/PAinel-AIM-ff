# Instruções para Executar no Visual Studio

## Pré-requisitos
1. **Visual Studio 2022** (Community, Professional ou Enterprise)
2. **.NET 8.0 SDK** instalado
3. **BlueStacks 4** instalado
4. **Free Fire** instalado no BlueStacks

## Passos para Executar

### 1. Abrir o Projeto
1. Abra o Visual Studio 2022
2. Clique em "Abrir um projeto ou solução"
3. Navegue até a pasta do projeto e selecione `painelff.sln`
4. Clique em "Abrir"

### 2. Configurar a Solução
1. No **Solution Explorer**, clique com botão direito em `painelff`
2. Selecione **Properties**
3. Na aba **Build**, verifique se:
   - **Platform target**: x64
   - **Configuration**: Debug ou Release
4. Feche as Properties

### 3. Restaurar Pacotes NuGet
1. Clique com botão direito na solução no **Solution Explorer**
2. Selecione **Restore NuGet Packages**
3. Aguarde a restauração completar

### 4. Executar o Projeto
1. Pressione **F5** ou clique no botão **Start** (▶️)
2. Se aparecer um aviso sobre executar como administrador, clique **Sim**
3. O programa será compilado e executado

### 5. Usar o Aimbot
1. **Inicie o BlueStacks 4** e o **Free Fire**
2. **Entre em uma partida** no Free Fire
3. No programa, clique em **"Ativar Aimbot"**
4. Aguarde a mensagem de sucesso
5. O aimbot será aplicado automaticamente

## Solução de Problemas

### Erro: "Não foi possível restaurar os pacotes NuGet"
- Verifique sua conexão com a internet
- Tente: **Tools > NuGet Package Manager > Package Manager Console**
- Digite: `Update-Package -reinstall`

### Erro: "Access Denied" ou "Permissão Negada"
- Execute o Visual Studio como **Administrador**
- Clique com botão direito no Visual Studio > "Executar como administrador"

### Erro: "BlueStacks 4 não encontrado"
- Certifique-se de que o BlueStacks 4 está rodando
- Verifique se o processo se chama "HD-Player"
- Reinicie o BlueStacks se necessário

### Erro: "Nenhum endereço encontrado"
- Certifique-se de que o Free Fire está rodando
- Entre em uma partida no Free Fire
- A AOB pode precisar ser atualizada para versões mais recentes

## Configurações Recomendadas

### Para Melhor Performance:
1. **Configuration**: Release
2. **Platform**: x64
3. **Target Framework**: .NET 8.0

### Para Debug:
1. **Configuration**: Debug
2. Adicione breakpoints no código se necessário
3. Use **Output Window** para ver logs de debug

## Compilação Manual
Se preferir compilar manualmente:
1. Abra **Developer Command Prompt for VS 2022**
2. Navegue até a pasta do projeto
3. Execute: `dotnet build --configuration Release --platform x64`
4. O executável estará em: `bin\Release\net8.0-windows\`

## Notas Importantes
- **Execute sempre como administrador** para acessar a memória do BlueStacks
- O programa funciona apenas com **BlueStacks 4**
- A AOB pode precisar ser atualizada para versões mais recentes do Free Fire
- Use por sua conta e risco - pode violar os termos do jogo 