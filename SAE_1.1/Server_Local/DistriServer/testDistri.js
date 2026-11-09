function testDistri(){

	const getUnDistri = async () => {
	try{
		const response = await fetch(`http://localhost:8080/distri/1`);        
		//Vérifier si la réponse est correcte
		if(!response.ok) {
			throw new Error('Erreur de réseau !');
		}
		//Sinon Retourner le résultat au format JSON
		const data = await response.json();
		//Traiter les données récupérées
		const ContainerDistri= document.getElementById('ContainerDistri');
			codeHTML = `
					<p>Id: ${data.infosDistributeur.id}</p>
					<p>Nom organisation : ${data.infosDistributeur.nomOrganisation}</p>
					<p>Localisation : ${data.infosDistributeur.localisation}</p>
					<p>Adresse : ${data.infosDistributeur.adresse}</p>
					
					<p>Propose les produits suivants :</p>
					<ul> `;

					for(i = 0; i < data.produits.length; i++){
						codeHTML = codeHTML + "<li>" + data.produits[i].nom + "</li>";
					}
					codeHTML = codeHTML + "</ul>";					

			 // Afficher le code HTML dans le container
			ContainerDistri.innerHTML = codeHTML;
	}catch (error)	{// Gérer les erreurs (réseau, réponse non correcte, etc.)    
		console.error('Une erreur est survenue :', error);  
		}
	};
	
	// Appeler la fonction pour récupérer et afficher le distributeur
	getUnDistri();
}
