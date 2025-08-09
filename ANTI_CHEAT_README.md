# Sistema Anti-Cheat - PAinel-AIM-ff

## Visão Geral

O sistema anti-cheat implementado no PAinel-AIM-ff é uma solução robusta para proteger contra detecção e banimento em jogos. Ele utiliza técnicas avançadas de proteção de memória e detecção de ferramentas proibidas.

## Características Principais

### 🔒 Proteções Implementadas

O sistema inclui **27 proteções diferentes** (st0 a st26) que:

- **Substituem padrões de memória** específicos que podem ser usados para detecção
- **Aplicam patches de segurança** em tempo real
- **Monitoram continuamente** o processo do jogo
- **Detectam e neutralizam** tentativas de análise externa

### 🛡️ Detecção de Ferramentas Proibidas

O sistema detecta e bloqueia automaticamente:

- **Cheat Engine** e variantes
- **Process Hacker** e similares
- **IDA Pro** (32 e 64 bits)
- **OllyDbg** e **x64dbg**
- **Scylla** e **MegaDumper**
- **Artemis** e **ReClass.NET**
- **Ghidra** e **Radare2**
- **WinDbg** e **Immunity Debugger**

### ⚡ Proteção Contínua

- **Timer de proteção**: Aplica proteções a cada 5 segundos
- **Timer de detecção**: Verifica ferramentas proibidas a cada 2 segundos
- **Proteção automática**: Inicializa automaticamente com o painel

## Como Usar

### 1. Inicialização Automática

O sistema anti-cheat é inicializado automaticamente quando o painel é aberto. Não é necessária intervenção manual.

### 2. Gerenciador de Proteções

Para acessar o gerenciador de proteções:

1. Abra o painel principal
2. Clique no botão "Anti-Cheat" (se disponível)
3. Ou use o método programático: `AntiCheat.InitializeAsync()`

### 3. Aplicação Manual de Proteções

```csharp
// Aplicar todas as proteções
await AntiCheat.ApplyAllProtectionsAsync();

// Aplicar proteção específica
await AntiCheat.ApplyProtection("st0");

// Verificar status
var status = AntiCheat.GetProtectionStatus();
```

### 4. Monitoramento de Eventos

```csharp
// Evento quando proteção é aplicada
AntiCheat.ProtectionEvent += (sender, message) => {
    Console.WriteLine($"Proteção: {message}");
};

// Evento quando ferramenta proibida é detectada
AntiCheat.DetectionEvent += (sender, message) => {
    Console.WriteLine($"Detecção: {message}");
};
```

## Estrutura das Proteções

### Padrões de Assinatura

Cada proteção (st0-st26) possui:

- **Padrão de busca**: Sequência de bytes específica na memória
- **Padrão de substituição**: Bytes de segurança para aplicar
- **Status de ativação**: Indica se a proteção está funcionando

### Exemplo de Proteção

```csharp
// st0 - Proteção contra análise de memória
{
    "signature": [0x00, 0x48, 0x2D, 0xE9, 0x0D, 0xB0, 0xA0, 0xE1, ...],
    "replacement": [0x00, 0x00, 0xA0, 0xE3, 0x1E, 0xFF, 0x2F, 0xE1]
}
```

## Configuração Avançada

### Personalização de Proteções

Para adicionar novas proteções:

1. Adicione o padrão em `_signaturePatterns`
2. Adicione o replacement em `_replacementPatterns`
3. O sistema aplicará automaticamente

### Ajuste de Timers

```csharp
// Proteções a cada 5 segundos (padrão)
_protectionTimer = new Timer(async _ => await ApplyAllProtectionsAsync(), 
    null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

// Detecção a cada 2 segundos (padrão)
_detectionTimer = new Timer(_ => DetectForbiddenTools(), 
    null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
```

## Segurança e Privacidade

### Proteções Implementadas

- **Ofuscação de padrões**: Padrões são armazenados de forma segura
- **Verificação de integridade**: Validação contínua das proteções
- **Logs seguros**: Eventos são registrados sem expor informações sensíveis
- **Cleanup automático**: Recursos são liberados ao fechar o painel

### Recomendações de Uso

1. **Mantenha o painel atualizado** com as últimas proteções
2. **Não compartilhe** padrões de proteção específicos
3. **Monitore logs** para detectar tentativas de bypass
4. **Use em conjunto** com outras medidas de segurança

## Troubleshooting

### Problemas Comuns

#### Proteções não estão sendo aplicadas
- Verifique se o processo "HD-Player" está rodando
- Confirme se o sistema anti-cheat foi inicializado
- Verifique logs para erros específicos

#### Ferramentas não estão sendo detectadas
- Verifique se os nomes estão na lista `_forbiddenProcesses`
- Confirme se o timer de detecção está ativo
- Verifique permissões de acesso ao sistema

#### Erro de memória
- Verifique se o processo tem permissões adequadas
- Confirme se não há conflitos com outros programas
- Reinicie o painel se necessário

### Logs e Debug

O sistema registra eventos importantes:

```
[ANTI-CHEAT] Sistema inicializado com sucesso
[ANTI-CHEAT] Proteção st0 aplicada com sucesso
[DETECÇÃO] Ferramenta proibida detectada: cheatengine
[ANTI-CHEAT] Erro ao aplicar proteções: Access denied
```

## Suporte e Manutenção

### Atualizações

- **Proteções**: Atualizadas conforme necessário
- **Detecções**: Lista de ferramentas proibidas expandida
- **Performance**: Otimizações contínuas implementadas

### Contribuições

Para contribuir com o sistema anti-cheat:

1. **Reporte bugs** com logs detalhados
2. **Sugira novas proteções** baseadas em ameaças conhecidas
3. **Teste em diferentes ambientes** e reporte problemas
4. **Mantenha sigilo** sobre detalhes técnicos específicos

## Licença e Uso

Este sistema anti-cheat é parte do PAinel-AIM-ff e deve ser usado de acordo com os termos da licença do projeto principal.

---

**⚠️ AVISO**: Este sistema é destinado apenas para fins educacionais e de proteção legítima. O uso inadequado pode resultar em violação de termos de serviço de jogos.
