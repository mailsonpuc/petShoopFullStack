import './loading.css';

export function Loading() {
    return (
        <div className="loading-container">
            <div className="svg-loading-animation">
                <svg viewBox="0 0 64 48" className="animated-line-svg">
                    <style>
                        {`
                            :is(#back2087, #front2087) {
                                fill: none;
                                stroke-width: 3;
                                stroke-linecap: round;
                                stroke-linejoin: round;
                            }

                            #back2087 {
                                fill: none;
                                stroke: currentColor;
                                opacity: 0.1;
                            }

                            #front2087 {
                                fill: none;
                                stroke: currentColor;
                                stroke-dasharray: 48, 144;
                                stroke-dashoffset: 192;
                                animation: dash_682 1.4s linear infinite;
                            }

                            @keyframes dash_682 {
                                72.5% {
                                    opacity: 0;
                                }

                                to {
                                    stroke-dashoffset: 0;
                                }
                            }
                        `}
                    </style>
                    <polyline points="0.157 23.954, 14 23.954, 21.843 48, 43 0, 50 24, 64 24" id="back2087" />
                    <polyline points="0.157 23.954, 14 23.954, 21.843 48, 43 0, 50 24, 64 24" id="front2087" />
                </svg>
            </div>
            <div className="loading-progress-text">Carregando...</div>
        </div>
    );
}
