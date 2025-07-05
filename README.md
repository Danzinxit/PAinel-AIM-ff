# Free Fire Aimbot Panel

Um painel de controle para hacks do Free Fire no BlueStacks 4, desenvolvido em C# com interface gráfica moderna.

## Funcionalidades

### 🎯 Aimbot
- **Ativação Automática**: Escaneia e ativa o aimbot automaticamente
- **Múltiplos Alvos**: Suporte para diferentes partes do corpo (pescoço, ombros)
- **Reaplicação**: Possibilidade de reaplicar o aimbot durante o jogo
- **Status em Tempo Real**: Monitoramento do status do aimbot

### 🔫 No Recoil
- **Eliminação do Recuo**: Remove completamente o recuo das armas
- **Compatibilidade**: Funciona com todas as armas do jogo
- **Aplicação Instantânea**: Ativação com um clique

### 👁️ Vision Hack
- **Visão Melhorada**: Permite ver através de obstáculos
- **Padrão Otimizado**: Usa padrões AOB específicos para máxima eficiência
- **Ativação Segura**: Implementação que evita detecção

### 🧱 Wall Hack
- **Atravessar Paredes**: Permite ver e atirar através de paredes
- **Padrão Especializado**: Implementação específica para paredes
- **Compatibilidade Total**: Funciona em todos os mapas

## Requisitos

- **Sistema Operacional**: Windows 10/11 (x64)
- **Emulador**: BlueStacks 4
- **Jogo**: Free Fire
- **Privilégios**: Executar como Administrador
- **.NET**: Runtime 8.0 ou superior

## Instalação

1. **Baixe o projeto**:
   ```bash
   git clone [URL_DO_REPOSITORIO]
   cd PAinel-AIM-ff
   ```

2. **Compile o projeto**:
   ```bash
   dotnet build painelff.sln
   ```

3. **Execute como Administrador**:
   - Navegue até `painelff\bin\Debug\net8.0-windows\`
   - Execute `painelff.exe` como administrador

## Como Usar

### Preparação
1. Inicie o BlueStacks 4
2. Abra o Free Fire
3. Execute o painel como administrador

### Ativação dos Hacks

#### Aimbot
1. Clique em **"Ativar Aimbot"**
2. Aguarde a mensagem de sucesso
3. Use **"Aplicar Novamente"** se necessário

#### No Recoil
1. Clique em **"Ativar No Recoil"**
2. O recuo será eliminado instantaneamente

#### Vision Hack
1. Clique em **"Ativar Vision Hack"**
2. Aguarde a confirmação de ativação
3. A visão melhorada estará ativa

#### Wall Hack
1. Clique em **"Ativar Wall Hack"**
2. Aguarde a confirmação de ativação
3. Poderá ver através de paredes

### Monitoramento
- Use **"Status do Sistema"** para verificar o estado de todos os hacks
- O painel mostra o status em tempo real de cada funcionalidade

## Estrutura do Projeto

```
PAinel-AIM-ff/
├── painelff/
│   ├── Form1.cs              # Lógica principal dos hacks
│   ├── Form1.Designer.cs     # Interface gráfica
│   ├── Program.cs            # Ponto de entrada
│   └── painelff.csproj       # Configuração do projeto
├── Build_x64/                # Dependências compiladas
├── painelff.sln              # Solução do Visual Studio
└── README.md                 # Este arquivo
```

## Padrões AOB Utilizados

### Vision Hack
- **Busca**: `00 00 B4 43 DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30`
- **Substituição**: `00 00 B4 43 00 00 A0 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30`

### Wall Hack
- **Busca**: `09 0E 00 00 80 3F 00 00 80 3F`
- **Substituição**: `09 0E 00 00 A0 4F 00 00 80 3F`

## Segurança

⚠️ **Aviso Importante**:
- Este software é para fins educacionais
- Use por sua conta e risco
- Pode resultar em banimento da conta
- Recomenda-se usar em contas secundárias

## Suporte

Para problemas ou dúvidas:
1. Verifique se o BlueStacks 4 está rodando
2. Certifique-se de executar como administrador
3. Confirme que o Free Fire está aberto
4. Verifique o status do sistema

## Changelog

### v2.0
- ✅ Adicionado Vision Hack
- ✅ Adicionado Wall Hack
- ✅ Interface atualizada
- ✅ Status melhorado
- ✅ Compilação otimizada

### v1.0
- ✅ Aimbot básico
- ✅ No Recoil
- ✅ Interface inicial

## Licença

Este projeto é fornecido "como está" sem garantias. Use por sua conta e risco.

---

**Desenvolvido para fins educacionais e de pesquisa em segurança de jogos.** 