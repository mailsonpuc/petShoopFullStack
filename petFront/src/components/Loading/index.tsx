import './loading.css';
import petLoading from '../../assets/PawPrintAnimation.gif'; // Adicione sua imagem aqui

export function Loading() {
    return (
        <div className="loading-container">
            <img src={petLoading} alt="Carregando pet" className="pet-loading-image" />
            <div className="loading-progress-text">Carregando...</div>
        </div>
    );
}
