import React from 'react'
import { Container } from 'react-bootstrap';
import './home.css'

const HomePage = () => {
    return (
        <>
            <section className='home-main_wrapper'>
                <Container>
                    <h1>Главная страница</h1>
                    <p className='home_text'>
                        Планирование реагирования на инциденты в сфере кибербезопасности (CSIRP)
                        представляет собой основную часть подготовки организации до того, как произойдет кибератака или инцидент.
                        Организации никогда не могут знать, с каким типом киберугрозы они столкнутся в следующий раз и когда это произойдет. Поэтому жизненно важно составить план действий или дорожную карту для всех возможных событий.
                    </p>
                </Container>
            </section>
            <section className='home-create-method_wrapper'>
            <Container>
                    <h1>Создание методики</h1>
                    <p className='home_text'>
                        Основные задачи для разработки программного обеспечения заключаются в подходах реализации лучших практик, условии и проектирование процессов:
                        <br />1. Применение методологий CSIRP (планированное реагирование на инциденты кибер-безопасности) для подготовки контрмер до того, как произошел инцидент.
                    </p>
                    <ul className="list-group list-group-flush">
                        <li className="list-group-item">- Получение одобрения</li>
                        <li className="list-group-item">- Анализ приоритетов</li>
                        <li className="list-group-item">- Эскалация инцидента безопасности</li>
                        <li className="list-group-item">- Реагирование и восстановление</li>
                        <li className="list-group-item">- Надежная связь (коммуникация)</li>
                        <li className="list-group-item">- Обзор и пересмотр</li>
                        <li className="list-group-item">- Ответственность</li>
                        <li className="list-group-item">- Определение инцидента безопасности</li>
                        <li className="list-group-item">- Определение и сфера применения</li>
                    </ul>
                </Container>
            </section>
        </>
    )
}

export { HomePage };

