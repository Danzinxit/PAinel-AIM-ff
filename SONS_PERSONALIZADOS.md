# 🔊 Guia de Sons Personalizados

## 🎵 Sons Atuais

O painel agora usa sons personalizados baseados em frequências diferentes:

### 📋 Tipos de Som
- **Clique**: Frequência 800Hz, duração 50ms (som curto e agudo)
- **Sucesso**: Frequências 600Hz → 800Hz → 1000Hz (som ascendente)
- **Erro**: Frequências 1000Hz → 800Hz → 600Hz (som descendente)
- **Ativação**: Frequência 700Hz, duração 150ms (som médio)

## 🎧 Como Adicionar Sons Personalizados

### 1. Preparar Arquivos de Som
- Formato: `.wav` (recomendado)
- Duração: 1-3 segundos
- Qualidade: 44.1kHz, 16-bit

### 2. Criar Pasta de Sons
```
painelff/
├── sounds/
│   ├── click.wav
│   ├── success.wav
│   ├── error.wav
│   └── activate.wav
```

### 3. Modificar o Código
No arquivo `Form1.cs`, na função `InitializeSounds()`:

```csharp
private void InitializeSounds()
{
    try
    {
        soundSuccess = new SoundPlayer();
        soundError = new SoundPlayer();
        soundClick = new SoundPlayer();
        soundActivate = new SoundPlayer();
        
        // Carregar sons personalizados
        soundClick.LoadAsync("sounds/click.wav");
        soundSuccess.LoadAsync("sounds/success.wav");
        soundError.LoadAsync("sounds/error.wav");
        soundActivate.LoadAsync("sounds/activate.wav");
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Erro ao inicializar sons: {ex.Message}");
    }
}
```

### 4. Modificar Funções de Som
Substituir as funções de som por:

```csharp
private void PlayClickSound()
{
    try
    {
        if (soundClick != null)
            soundClick.Play();
    }
    catch { }
}

private void PlaySuccessSound()
{
    try
    {
        if (soundSuccess != null)
            soundSuccess.Play();
    }
    catch { }
}

private void PlayErrorSound()
{
    try
    {
        if (soundError != null)
            soundError.Play();
    }
    catch { }
}

private void PlayActivateSound()
{
    try
    {
        if (soundActivate != null)
            soundActivate.Play();
    }
    catch { }
}
```

## 🎵 Sugestões de Sons

### Clique
- Som de "pop" suave
- Beep eletrônico curto
- Som de interface moderna

### Sucesso
- Som de "ding" ascendente
- Som de vitória
- Som de confirmação

### Erro
- Som de "buzz" descendente
- Som de alerta
- Som de negação

### Ativação
- Som de "power on"
- Som de inicialização
- Som de ativação de sistema

## 🔧 Configurações Avançadas

### Volume dos Sons
Para controlar o volume, você pode usar a API do Windows:

```csharp
[DllImport("winmm.dll")]
public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

// Volume de 0 a 65535 (0 = mudo, 65535 = máximo)
waveOutSetVolume(IntPtr.Zero, 32767); // 50% do volume
```

### Sons Diferentes por Botão
Você pode criar sons específicos para cada botão:

```csharp
private SoundPlayer? soundAimbot;
private SoundPlayer? soundNoRecoil;
private SoundPlayer? soundVisionHack;
private SoundPlayer? soundWallHack;

// No evento de clique de cada botão
private void btnActive_Click(object sender, EventArgs e)
{
    if (soundAimbot != null)
        soundAimbot.Play();
    // resto do código...
}
```

## ⚠️ Observações

- Os sons atuais usam `Console.Beep()` que funciona em qualquer Windows
- Para sons personalizados, certifique-se de que os arquivos existem
- Teste os sons em diferentes volumes do sistema
- Considere adicionar uma opção para desativar sons

## 🎮 Exemplo de Implementação Completa

```csharp
// Adicionar no construtor
private bool soundEnabled = true;

// Função para alternar sons
private void ToggleSound()
{
    soundEnabled = !soundEnabled;
    // Atualizar interface
}

// Modificar funções de som
private void PlayClickSound()
{
    if (!soundEnabled) return;
    
    try
    {
        if (soundClick != null)
            soundClick.Play();
        else
            Console.Beep(800, 50);
    }
    catch { }
}
```

---

**Dica**: Para sons profissionais, considere usar bibliotecas como NAudio ou Bass.Net para mais controle sobre reprodução de áudio. 