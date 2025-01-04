using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RentCars_Back.Models;

public class PdfReportService : IReportService
{
    public byte[] GeneratePdfReport(ReportRequest request)
    {
        var imagePath = "C:/Users/mi212/OneDrive/Área de Trabalho/imageHeader.png";

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.MarginTop(0); // Remove a margem superior, deixando as outras margens intactas
                page.MarginBottom(2, Unit.Centimetre); // Exemplo de margem inferior
                page.MarginLeft(1, Unit.Centimetre); // Exemplo de margem à esquerda
                page.MarginRight(1, Unit.Centimetre); // Exemplo de margem à direita
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header()
                    .Image(imagePath).UseOriginalImage().WithRasterDpi(10);

                page.Content()
                    // .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Spacing(5);

                        IContainer BlockStyle(IContainer container) => container.Background(Colors.Grey.Lighten3).Padding(4);

                        column.Item()
                            .Element(BlockStyle)
                            .AlignCenter()
                            .Text($"CONTRATO DE LOCAÇÃO DE AUTOMÓVEL POR PRAZO DETERMINADO")
                            .Bold(); 
                        column.Spacing(5);                        

                        column.Item().Text($"LOCADOR: CAMILA ALVES DE ARAÚJO BERNARDINO, brasileira, casada, autônoma, portadora do RG/SSP/SP sob n° 46.265.535-0, inscrita no CPF/MF sob o n° 379.891.198-38, residente e domiciliada no Município de Americana/SP – , endereço eletrônico camilabernardino44@gmail.com.");
                        column.Item().Text($"LOCATÁRIO: {request.Name}, Uso Particular , portador do RG sob o n°{"request.RG"} ssp, inscrito no CPF/MF sob n {"request.CPF"},CNH n°{"request.CNH"}. Residente na Rua {request.Street} n {request.Number}, BAIRRO {request.District}, Cep{request.Cep}. Município de {request.City} {request.Uf} endereço eletrônico {"request.Email"}");
                        column.Item().Text("As partes acima identificadas têm, entre si, justo e acertado o presente Contrato de Locação de Automóvel sepor Prazo Determinado, que se regerá pelas cláusulas e condições especificadas abaixo, bem como de acordo com os dispositivos previstos no artigo 565 e seguintes do Código Civil (CC).");
                        column.Item().Text("DO OBJETO");
                        column.Item().Text("Cláusula 1ª. Por meio deste contrato regula-se a locação do veículo automotor Ford Ka SE , Cor Prata, Ano 2020, PLACA QXV5I52, RENAVAM 01226643040, propriedade do LOCADOR.");
                        column.Item().Text("Parágrafo único. O instrumento é acompanhado de um laudo de vistoria encaminhado pelo LOCADOR ao LOCATÁRIO por meio do aplicativo WhatsApp ou por e-mail, sendo estes informados no ato da contratação, em que se descreve o veículo e o seu estado de conservação quando o mesmo foi entregue ao LOCATÁRIO.");
                       
                        // DO VALOR DO ALUGUEL
                        column.Item().Text("DO VALOR DO ALUGUEL").Bold().Underline();
                        column.Item().Text("Cláusula 2ª. O valor do aluguel, livremente ajustado pelas partes, é de R$9.000 (Nove mil reais), " +
                                            "pagos da seguinte forma:");
                        column.Item().Text("- 12 (doze) parcelas semanais de R$750,00 (Setecentos e Cinquenta reais), com vencimento toda Sexta-feira, " +
                                            "sendo a primeira realizada na primeira Sexta-feira subsequente ao pagamento da caução prevista na Clausula 3ª.");

                        column.Item().Text("Parágrafo Primeiro. O LOCATÁRIO deverá efetuar o pagamento do valor acordado, diretamente para o " +
                                            "LOCADOR ou terceiro autorizado por este, cujos dados bancários serão informados no ato da contratação.");
                        column.Item().Text("Parágrafo Segundo. O LOCATÁRIO, não vindo a efetuar o pagamento do aluguel por um período de atraso " +
                                            "superior à 02 (dois) dias, fica sujeito a ter a posse do veículo configurada como Apropriação Indébita, " +
                                            "implicando também a possibilidade de adoção de medidas judiciais, inclusive a Busca e Apreensão do veículo " +
                                            "e/ou lavratura de Boletim de Ocorrência, cabendo ao LOCATÁRIO ressarcir o LOCADOR das despesas oriundas " +
                                            "da retenção indevida do bem, arcando ainda com as despesas judiciais e/ou extrajudiciais que o LOCADOR " +
                                            "venha a ter para efetuar a busca, apreensão e efetiva reintegração da posse do veículo.");
                        column.Item().Text("Parágrafo Terceiro. Em caso de inadimplência, incorrerá em multa contratual de 2% (dois por cento) da parcela " +
                                            "em atraso, acrescidos de correção monetária pelo índice IPCA-e, além de juros moratórios diários de 0,033%. " +
                                        "E multa de 25.00 (vinte e cinco) reais por dia de atraso.");

                                        // Primeira pagina ^                                        

                        // Cláusula 3 - Caução e Limite de KM
                        column.Item().Text("DA CAUÇÃO E LIMITE DE KM", TextStyle.Default.Bold().Underline());
                        column.Item().Text("Cláusula 3ª. O presente contrato é garantido por CAUÇÃO no valor de R$1000.00 (mil reais), devido no ato de celebração deste contrato, que será devolvido ao LOCATÁRIO no prazo de 30 (trinta) dias após a entrega do carro no final da relação contratual, sendo autorizado o abatimento no valor de eventuais multas, encargos decorrentes da locação e desgastes pelo uso do veículo.");
                        column.Item().Text("O Caução não é devolvido se o veículo for devolvido antes do término de 3 meses. E na devolução do veículo a semana não for paga, não será devolvido o valor do caução.");
                        column.Item().Text("Limite de 1.800 Km Semanal, se ultrapassado será cobrado 0.50 centavos por Km ultrapassado.");
                        
                        // Cláusula 4 - Finalidade da locação
                        column.Item().Text("DA FINALIDADE DA LOCAÇÃO", TextStyle.Default.Bold().Underline());
                        column.Item().Text("Cláusula 4ª. O automóvel objeto deste contrato será utilizado pelo LOCATÁRIO na exclusiva finalidade profissional de motorista de aplicativos, somente no Município que firmou o presente contrato, não sendo permitido em nenhuma hipótese sua utilização por terceiros, sublocação e para outros fins diversos.");
                        column.Item().Text("Parágrafo Primeiro. Em razão de o veículo ser utilizado por motoristas de aplicativos, será permitida viagens realizadas/corridas até outras cidades do ESTADO DE SÃO PAULO, desde que o veículo seja retornado a cidade em que firmado o contrato no mesmo dia, estando vedada sua estadia/pernoite em outras localidades.");
                        column.Item().Text("Parágrafo Segundo. Na hipótese prevista no Parágrafo Primeiro desta Cláusula, está cientificado o LOCATÁRIO que o seguro automotivo contratado pelo LOCADOR possui limitação de distanciamento de 200 km. Em caso do LOCATÁRIO se deslocar acima da referida quilometragem, será exclusivamente responsável pelo pagamento de eventuais custos com guinchos e oficionas da localidade.");
                        
                        // Cláusula 5 - Prazo
                        column.Item().Text("DO PRAZO", TextStyle.Default.Bold().Underline());
                        column.Item().Text("Cláusula 5ª. A presente locação terá o prazo determinado de 03 (três) meses, iniciando-se em 20 (Vinte) de Dezembro de 2024, e com término em 20 (Vinte) de Março de 2025.");
                        column.Item().Text("Parágrafo Primeiro. Decorrido o término do prazo contratual, deverá o locatário proceder com a devolução nos termos previstos na Cláusula 6ª.");
                        column.Item().Text("Parágrafo Segundo. Em caso de interesse em renovação e extensão do prazo contratual, deverá o LOCATÁRIO informar o LOCADOR no prazo de 48 (quarenta e oito) horas anteriores ao prazo findo.");

                                    //Terceira Pagina
                        
                        // Cláusula 6ª - Devolução
                            column.Item().Text("DA DEVOLUÇÃO").Bold().Underline();
                            column.Item().Text("Cláusula 6ª. O LOCATÁRIO deverá devolver o veículo locado ao LOCADOR nas mesmas condições que se encontrava quando foi recebido, em perfeitas condições de nova locação, conforme laudo de vistoria realizada.");
                            column.Item().Text("Parágrafo Primeiro. O LOCATÁRIO declara estar ciente que quaisquer danos causados, materiais ou pessoais, decorrente da utilização do veículo ora locado, será de sua responsabilidade, inclusive lucros cessantes.");
                            column.Item().Text("Parágrafo Segundo. Desde já, o LOCADOR autoriza a retenção do valor caucionado para reparação dos danos mencionados acima.");

                            // Cláusula 7ª - Despesas com Manutenção
                            column.Item().Text("DAS DESPESAS COM MANUTENÇÃO DO VEÍCULO").Bold().Underline();
                            column.Item().Text("Cláusula 7ª. Fica estipulado que as despesas de manutenção * pneus e troca de óleo do veículo locado (desgastes naturais pelo uso do bem) serão dívidas entre as partes, devendo ser avisado ao LOCATÁRIO qualquer irregularidade anterior com o veículo.");
                            column.Item().Text("Em caso de danos no veículo, tais como arranhões e amassados, incidentes no curso do prazo contratual, será de responsabilidade exclusiva do LOCATÁRIO, devendo o mesmo ser feito de imediato.");
                            column.Item().Text("Manutenção do veículo será feita pelo proprietário em sua oficina; desde que não seja de mau uso.");
                            column.Item().Text("Não desconto dia de carro parado em mecânica e funilaria.");

                            // Cláusula 8ª - Combustível
                            column.Item().Text("DO COMBUSTÍVEL").Bold().Underline();
                            column.Item().Text("Cláusula 8ª. Ao final do prazo estipulado, o LOCATÁRIO deverá devolver o veículo ao LOCADOR com a mesma quantidade de combustível que foi retirado, sendo de sua exclusiva responsabilidade o abastecimento do bem enquanto vigente o presente instrumento.");
                            column.Item().Text("Parágrafo Primeiro. Caso seja constatado a utilização de combustível adulterado, o LOCATÁRIO responderá pelo mesmo e pelos danos decorrentes de tal utilização.");
                            column.Item().Text("Parágrafo Segundo. O LOCADOR orienta o LOCATÁRIO para que evite utilizar a reserva do tanque de combustível, dado que tal prática poderá acarretar danos no veículo.");

                            // Cláusula 9ª - Multas e Infrações
                            column.Item().Text("DAS MULTAS E INFRAÇÕES").Bold().Underline();
                            column.Item().Text("Cláusula 9ª. As multas ou quaisquer outras infrações às leis de trânsito, cometidas durante o período da locação do veículo, serão de responsabilidade do LOCATÁRIO, devendo ser liquidadas quando da notificação pelos órgãos competentes ou no final do contrato, o que ocorrer primeiro.");
                            column.Item().Text("Parágrafo Primeiro. Em caso de apreensão do veículo, serão cobradas do LOCATÁRIO todas as despesas de serviço dos profissionais envolvidos para liberação do veículo alugado, assim como todas as taxas cobradas pelos órgãos competentes, e quantas diárias forem necessárias até a disponibilização do veículo para locação.");
                            column.Item().Text("Parágrafo Segundo. O LOCATÁRIO declara-se ciente e concorda que se ocorrer qualquer multa ou infração de trânsito durante a vigência deste contrato, seu nome será indicado pelo LOCADOR junto ao Órgão de Trânsito autuante, na qualidade de condutor do veículo, tendo assim a pontuação recebida transferida para sua carteira de habilitação.");
                            column.Item().Text("Parágrafo Terceiro. O LOCADOR preencherá os dados relativos à 'apresentação do Condutor', previsto na Resolução 404/12 do CONTRAN, caso tenha sido lavrada autuação por infrações de trânsito enquanto o veículo esteve em posse e responsabilidade do LOCATÁRIO.");
                            
                            //Quarta Pagina

                            // Cláusula 6ª - Devolução
                            column.Item().Text("DA DEVOLUÇÃO").Bold().Underline();
                            column.Item().Text("Cláusula 6ª. O LOCATÁRIO deverá devolver o veículo locado ao LOCADOR nas mesmas condições que se encontrava quando foi recebido, em perfeitas condições de nova locação, conforme laudo de vistoria realizada.");
                            column.Item().Text("Parágrafo Primeiro. O LOCATÁRIO declara estar ciente que quaisquer danos causados, materiais ou pessoais, decorrente da utilização do veículo ora locado, será de sua responsabilidade, inclusive lucros cessantes.");
                            column.Item().Text("Parágrafo Segundo. Desde já, o LOCADOR autoriza a retenção do valor caucionado para reparação dos danos mencionados acima.");

                            // Cláusula 7ª - Despesas com Manutenção
                            column.Item().Text("DAS DESPESAS COM MANUTENÇÃO DO VEÍCULO").Bold().Underline();
                            column.Item().Text("Cláusula 7ª. Fica estipulado que as despesas de manutenção * pneus e troca de óleo do veículo locado (desgastes naturais pelo uso do bem) serão dívidas entre as partes, devendo ser avisado ao LOCATÁRIO qualquer irregularidade anterior com o veículo.");
                            column.Item().Text("Em caso de danos no veículo, tais como arranhões e amassados, incidentes no curso do prazo contratual, será de responsabilidade exclusiva do LOCATÁRIO, devendo o mesmo ser feito de imediato.");
                            column.Item().Text("Manutenção do veículo será feita pelo proprietário em sua oficina; desde que não seja de mau uso.");
                            column.Item().Text("Não desconto dia de carro parado em mecânica e funilaria.");

                            // Cláusula 8ª - Combustível
                            column.Item().Text("DO COMBUSTÍVEL").Bold().Underline();
                            column.Item().Text("Cláusula 8ª. Ao final do prazo estipulado, o LOCATÁRIO deverá devolver o veículo ao LOCADOR com a mesma quantidade de combustível que foi retirado, sendo de sua exclusiva responsabilidade o abastecimento do bem enquanto vigente o presente instrumento.");
                            column.Item().Text("Parágrafo Primeiro. Caso seja constatado a utilização de combustível adulterado, o LOCATÁRIO responderá pelo mesmo e pelos danos decorrentes de tal utilização.");
                            column.Item().Text("Parágrafo Segundo. O LOCADOR orienta o LOCATÁRIO para que evite utilizar a reserva do tanque de combustível, dado que tal prática poderá acarretar danos no veículo.");

                            // Cláusula 9ª - Multas e Infrações
                            column.Item().Text("DAS MULTAS E INFRAÇÕES").Bold().Underline();
                            column.Item().Text("Cláusula 9ª. As multas ou quaisquer outras infrações às leis de trânsito, cometidas durante o período da locação do veículo, serão de responsabilidade do LOCATÁRIO, devendo ser liquidadas quando da notificação pelos órgãos competentes ou no final do contrato, o que ocorrer primeiro.");
                            column.Item().Text("Parágrafo Primeiro. Em caso de apreensão do veículo, serão cobradas do LOCATÁRIO todas as despesas de serviço dos profissionais envolvidos para liberação do veículo alugado, assim como todas as taxas cobradas pelos órgãos competentes, e quantas diárias forem necessárias até a disponibilização do veículo para locação.");
                            column.Item().Text("Parágrafo Segundo. O LOCATÁRIO declara-se ciente e concorda que se ocorrer qualquer multa ou infração de trânsito durante a vigência deste contrato, seu nome será indicado pelo LOCADOR junto ao Órgão de Trânsito autuante, na qualidade de condutor do veículo, tendo assim a pontuação recebida transferida para sua carteira de habilitação.");
                            column.Item().Text("Parágrafo Terceiro. O LOCADOR preencherá os dados relativos à 'apresentação do Condutor', previsto na Resolução 404/12 do CONTRAN, caso tenha sido lavrada autuação por infrações de trânsito enquanto o veículo esteve em posse e responsabilidade do LOCATÁRIO.");
                            column.Item().Text("Parágrafo Quarto. Descabe qualquer discussão sobre a procedência ou improcedência das infrações de trânsito aplicadas, e poderá o LOCATÁRIO, a seu critério e às suas expensas, recorrer das Multas junto ao Órgão de Trânsito competente, o que não o eximirá do pagamento do valor da multa, mas lhe dará o direito ao reembolso, caso o recurso seja julgado procedente.");

                            // Cláusula 10ª - Sinistro
                            column.Item().Text("DO SINISTRO").Bold().Underline();
                            column.Item().Text("Cláusula 10ª. Na hipótese de não devolução, roubo, furto, ou destruição total ou parcial do veículo por ora locado, deverá o LOCATÁRIO colaborar com todos os trâmites necessários exigidos pela seguradora précontratada para o percebimento da indenização securitária devida.");
                            column.Item().Text("Parágrafo Primeiro. Em caso da não colaboração mencionada, fica desde já estipulada indenização devida pelo LOCATÁRIO ao LOCADOR no valor do bem locado avaliado por meio da TABELA FIPE na data do sinistro.");
                            column.Item().Text("Parágrafo Segundo. Havendo sinistro decorrente de culpa ou dolo do LOCATÁRIO, ficará este responsável pelo pagamento da franquia do seguro.");
                            column.Item().Text("Parágrafo Terceiro. O LOCATÁRIO ficará responsável pelos danos em que a seguradora contratada pelo LOCADOR não cobrir, inclusive lucros cessantes decorrentes da impossibilidade de nova locação do bem.");

                            // Cláusula 11ª - Rescisão Contratual
                            column.Item().Text("DA RESCISÃO CONTRATUAL").Bold().Underline();
                            column.Item().Text("Cláusula 11ª. O descumprimento de qualquer cláusula por parte do LOCATÁRIO, sobretudo quanto o não pagamento dos valores acordados, bem como a falta de comunicação, recusa a atender telefone, e ausência de respostas encaminhadas pelo aplicativo WhatsApp, acarretará a rescisão deste contrato e o pagamento de multa rescisória devida pelo LOCATÁRIO no valor de R$2.500,00 (dois mil e quinhentos reais).");

                            // Cláusula 12ª - Vigência e Eleição de Foro
                            column.Item().Text("DA VIGÊNCIA & ELEIÇÃO DE FORO").Bold().Underline();
                            column.Item().Text("Cláusula 12ª. O presente contrato passará a vigorar a partir da data de sua assinatura e é celebrado pelo prazo de duração da locação contratada, gerando seus efeitos até a sua conclusão.");
                            column.Item().Text("As partes elegem o Foro da Comarca de Americana/SP para dirimir quaisquer controvérsias oriundas do contrato.");
                            column.Item().Text("E, por estarem justos de contratados, assinam o presente contrato em 02 (duas) vias de igual teor, juntamente com as 02 (duas) testemunhas abaixo.");
                            
                            // Local e data
                            column.Item()
                                  .PaddingBottom(15)
                                  .Text("Americana, _______ de ____________________ de 2.024.");

                            // Assinaturas
                            column.Item()
                                  .AlignCenter()
                                  .Row(row =>
                            {
                                row.RelativeItem().Text("____________________________________________________");
                                row.RelativeItem().Text("____________________________________________________");
                            });

                            column.Item()
                                  .PaddingBottom(15)
                                  .AlignCenter()
                                  .Row(row =>
                            {
                                row.RelativeItem().Text("LOCADOR: Camila");
                                row.RelativeItem().Text("LOCATÁRIO: Cláudio");
                            });

                            column.Item()
                                  .AlignCenter()
                                  .Row(row =>
                            {
                                row.RelativeItem().Text("____________________________________________________");
                                row.RelativeItem().Text("____________________________________________________");
                            });

                            column.Item()
                                  .AlignCenter()
                                  .Text("TESTEMUNHAS");

                        
                    
                    });

                    

                // page.Footer()
                //     .AlignCenter()
                //     .Text(x =>
                //     {
                //         x.Span("Página ");
                //         x.CurrentPageNumber();
                //     });
            });
        });

        return document.GeneratePdf();
    }
}
