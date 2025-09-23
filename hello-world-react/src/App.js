import './App.css';
import HelloWorld from './components/HelloWorld';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Hello World!</h1>
        <p>Bem-vindo ao meu primeiro projeto React! 🚀</p>
        <p>Este é um componente Hello World criado com React.</p>
        
        <HelloWorld name="React Developer" />
        
        <div style={{ marginTop: '20px' }}>
          <button 
            onClick={() => alert('Olá, React!')}
            style={{
              padding: '10px 20px',
              fontSize: '16px',
              backgroundColor: '#61dafb',
              border: 'none',
              borderRadius: '5px',
              cursor: 'pointer',
              color: '#20232a'
            }}
          >
            Clique aqui!
          </button>
        </div>
      </header>
    </div>
  );
}

export default App;
