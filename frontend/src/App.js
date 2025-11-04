import { useState, useEffect } from 'react';
import './App.css';

const API_BASE_URL = 'https://localhost:7191/api/Geeni';

function App() {
  const [geenid, setGeenid] = useState([]);
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');
  
  const [nimetus, setNimetus] = useState('');
  const [alleel1Pos, setAlleel1Pos] = useState(false);
  const [alleel2Pos, setAlleel2Pos] = useState(false);
  
  const [vanem1, setVanem1] = useState('');
  const [vanem2, setVanem2] = useState('');
  
  //const [searchName, setSearchName] = useState('');
  const [randomResult, setRandomResult] = useState(null);

  const loadGeenid = async () => {
    setLoading(true);
    try {
      const res = await fetch(API_BASE_URL);
      if (res.ok) {
        const data = await res.json();
        setGeenid(data);
        setMessage('');
      } else {
        setMessage('Viga laadimisel');
      }
    } catch (err) {
      setMessage('Viga: ' + err.message);
    }
    setLoading(false);
  };

  useEffect(() => {
    loadGeenid();
  }, []);

  const createGeen = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/loo`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          alleeliNimetus: nimetus,
          vanem1Positiivne: alleel1Pos,
          vanem2Positiivne: alleel2Pos
        })
      });
      if (res.ok) {
        setMessage('Geen loodud!');
        setNimetus('');
        setAlleel1Pos(false);
        setAlleel2Pos(false);
        loadGeenid();
      } else {
        setMessage('Viga loomisel');
      }
    } catch (err) {
      setMessage('Viga: ' + err.message);
    }
    setLoading(false);
  };

  const combineGeenid = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/yhenda`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          vanem1Id: parseInt(vanem1),
          vanem2Id: parseInt(vanem2)
        })
      });
      if (res.ok) {
        setMessage('Geenid ühendatud!');
        setVanem1('');
        setVanem2('');
        loadGeenid();
      } else {
        setMessage('Viga ühendamisel');
      }
    } catch (err) {
      setMessage('Viga: ' + err.message);
    }
    setLoading(false);
  };
/*
const searchGeenid = async (e) => {
  e.preventDefault();
  setLoading(true);
  try {
    const res = await fetch(`${API_BASE_URL}/nimetus/${searchName}`);
    if (res.ok) {
      const data = await res.json();
      setGeenid(data);
      setMessage(`Leitud ${data.length} geeni`);
    } else {
      setMessage('Geene ei leitud');
  }
} catch (err) {
  setMessage('Viga: ' + err.message);
}
setLoading(false);
};
*/

  const getRandomAlleel = async (id) => {
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/${id}/juhuslikalleel`);
      if (res.ok) {
        const data = await res.json();
        setRandomResult(data);
        setMessage('');
      } else {
        setMessage('Viga');
      }
    } catch (err) {
      setMessage('Viga: ' + err.message);
    }
    setLoading(false);
  };

  const deleteGeen = async (id) => {
    if (!window.confirm('Kustuta geen?')) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/${id}`, { method: 'DELETE' });
      if (res.ok) {
        setMessage('Geen kustutatud');
        loadGeenid();
      } else {
        setMessage('Viga kustutamisel');
      }
    } catch (err) {
      setMessage('Viga: ' + err.message);
    }
    setLoading(false);
  };

  return (
    <div className="App">
      <h1>Geenide Haldamine</h1>
      
      {loading && <div className="message">Laeb...</div>}
      {message && <div className="message">{message}</div>}
      
      <div className="container">
        <div className="section">
          <h2>Loo Uus Geen</h2>
          <form onSubmit={createGeen}>
            <input 
              type="text" 
              placeholder="Nimetus (nt. reesus)" 
              value={nimetus}
              onChange={e => setNimetus(e.target.value)}
              required 
            />
            <label>
              <input type="checkbox" checked={alleel1Pos} onChange={e => setAlleel1Pos(e.target.checked)} />
              Alleel 1 Positiivne
            </label>
            <label>
              <input type="checkbox" checked={alleel2Pos} onChange={e => setAlleel2Pos(e.target.checked)} />
              Alleel 2 Positiivne
            </label>
            <button type="submit">Loo</button>
          </form>
        </div>

        <div className="section">
          <h2>Ühenda Geenid</h2>
          <form onSubmit={combineGeenid}>
            <select value={vanem1} onChange={e => setVanem1(e.target.value)} required>
              <option value="">Vali Vanem 1</option>
              {geenid.map(g => <option key={g.id} value={g.id}>#{g.id} - {g.alleel1.nimetus}</option>)}
            </select>
            <select value={vanem2} onChange={e => setVanem2(e.target.value)} required>
              <option value="">Vali Vanem 2</option>
              {geenid.map(g => <option key={g.id} value={g.id}>#{g.id} - {g.alleel1.nimetus}</option>)}
            </select>
            <button type="submit">Ühenda</button>
          </form>
        </div>

{/*
        <div className="section">
          <h2>Otsi Geene</h2>
          <form onSubmit={searchGeenid}>
            <input 
              type="text" 
              placeholder="Nimetus" 
              value={searchName}
              onChange={e => setSearchName(e.target.value)}
              required 
            />
            <button type="submit">Otsi</button>
            <button type="button" onClick={loadGeenid}>Näita Kõiki</button>
          </form>
        </div>
*/}

        {randomResult && (
          <div className="section random">
            <h3>Juhuslik Alleel</h3>
            <p><strong>{randomResult.nimetus}</strong> - {randomResult.positiivne ? 'On Positiivne' : 'Ei Negatiivne'}</p>
          </div>
        )}

        <div className="section wide">
          <h2>Geenid ({geenid.length})</h2>
          <div className="genes">
            {geenid.map(g => (
              <div key={g.id} className={`gene ${g.onPositiivne ? 'pos' : 'neg'}`}>
                <div className="gene-header">
                  <strong>Geen #{g.id}</strong>
                  <span>{g.onPositiivne ? 'On' : 'On'}</span>
                </div>
                <div className="gene-body">
                  <div>Alleel 1: {g.alleel1.nimetus} {g.alleel1.positiivne ? '(+)' : '(-)'}</div>
                  <div>Alleel 2: {g.alleel2.nimetus} {g.alleel2.positiivne ? '(+)' : '(-)'}</div>
                </div>
                <div className="gene-actions">
                  <button onClick={() => getRandomAlleel(g.id)}>Juhuslik</button>
                  <button onClick={() => deleteGeen(g.id)} className="delete">Kustuta</button>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
