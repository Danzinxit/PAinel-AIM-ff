# Instruções para Distribuição do Executável

## 📁 Localização do Executável

O executável compilado está localizado em:
```
painelff\bin\Release\net8.0-windows\
```

## 📦 Arquivos Necessários para Distribuição

Para que o programa funcione corretamente em outros computadores, você precisa distribuir **TODOS** estes arquivos:

### Arquivos Principais:
- `painelff.exe` - Executável principal
- `painelff.dll` - Biblioteca principal
- `painelff.deps.json` - Configuração de dependências
- `painelff.runtimeconfig.json` - Configuração do runtime

### Bibliotecas Externas:
- `Memory.dll` - Biblioteca para acesso à memória
- `Memory.xml` - Configuração da biblioteca Memory
- `Guna.UI2.dll` - Interface gráfica
- `Newtonsoft.Json.dll` - Processamento JSON
- `System.Management.dll` - Gerenciamento do sistema

### Pasta de Runtimes:
- `runtimes\` - Pasta completa com runtimes do .NET

## 🚀 Como Distribuir

### Opção 1: Pasta Completa (Recomendado)
1. Copie toda a pasta `painelff\bin\Release\net8.0-windows\`
2. Compacte em um arquivo ZIP
3. Envie o ZIP para as pessoas

### Opção 2: Executável Único (Avançado)
Para criar um executável único que inclui todas as dependências:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## 📋 Requisitos do Sistema

### Para quem receber o executável:

1. **Windows 10/11** (64-bit)
2. **.NET 8.0 Runtime** (se não usar self-contained)
3. **BlueStacks 4** instalado
4. **Free Fire** instalado no BlueStacks
5. **Permissões de Administrador**

## 🔧 Instalação do .NET Runtime

Se a pessoa não tiver o .NET 8.0 Runtime, ela pode baixar em:
https://dotnet.microsoft.com/download/dotnet/8.0

## ⚠️ Instruções Importantes para o Usuário Final

### 1. Extrair Arquivos
- Extraia todos os arquivos do ZIP em uma pasta
- **NÃO** separe os arquivos - mantenha todos juntos

### 2. Executar como Administrador
- Clique com botão direito no `painelff.exe`
- Selecione "Executar como administrador"
- **IMPORTANTE**: Sempre execute como administrador

### 3. Configurar BlueStacks
- Inicie o BlueStacks 4
- Abra o Free Fire
- Entre em uma partida

### 4. Usar o Programa
- Execute o `painelff.exe` como administrador
- Faça login com suas credenciais
- Clique em "Ativar Aimbot" quando estiver em uma partida

## 🛠️ Solução de Problemas

### Erro: "Não foi possível carregar a DLL"
- Verifique se todos os arquivos estão na mesma pasta
- Execute como administrador

### Erro: "BlueStacks não encontrado"
- Certifique-se de que o BlueStacks 4 está rodando
- Verifique se o processo se chama "HD-Player"

### Erro: "Acesso negado"
- Execute como administrador
- Desative temporariamente o antivírus

### Erro: ".NET Runtime não encontrado"
- Instale o .NET 8.0 Runtime
- Ou use a versão self-contained

## 📝 Notas de Segurança

- **Sempre execute como administrador**
- **Desative temporariamente o antivírus** se necessário
- **Use por sua conta e risco** - pode violar os termos do jogo
- **Mantenha o programa atualizado** para compatibilidade

## 🔄 Atualizações

Para atualizar o programa:
1. Recompile usando o script `CRIAR_EXECUTAVEL.bat`
2. Substitua os arquivos antigos pelos novos
3. Mantenha a mesma estrutura de pastas

## 📞 Suporte

Se houver problemas:
1. Verifique se todos os arquivos estão presentes
2. Execute como administrador
3. Verifique se o BlueStacks 4 está rodando
4. Entre em uma partida do Free Fire antes de ativar 