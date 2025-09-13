(function(window){
	const apiBaseUrl = window.apiBaseUrl || '';

	async function getJson(url){
		const res = await fetch(url);
		if(!res.ok) throw new Error(`Failed request: ${res.status}`);
		return res.json();
	}

	async function fetchUserRoles(){
		return getJson(`${apiBaseUrl}/api/UserRole`);
	}

	async function fetchProgramTypes(){
		return getJson(`${apiBaseUrl}/api/ProgramType`);
	}

	async function fetchSetaBodies(){
		return getJson(`${apiBaseUrl}/api/SetaBody`);
	}

	async function fetchUsers(){
		return getJson(`${apiBaseUrl}/api/User`);
	}

	async function fetchUserById(id){
		return getJson(`${apiBaseUrl}/api/User/${id}`);
	}

	function resolveRoleNameById(roles, roleId){
		if(!roles) return null;
		const match = roles.find(r => String(r.id) === String(roleId));
		return match ? (match.name || match.code || String(roleId)) : String(roleId);
	}

	function resolveRoleCodeById(roles, roleId){
		if(!roles) return null;
		const match = roles.find(r => String(r.id) === String(roleId));
		return match ? (match.code || match.name) : null;
	}

	const allowedAssignmentRoleCodes = ["Lecturer","Facilitator","Assessor","Moderator"];
	function isAllowedAssigneeRoleCode(roleCode){
		return allowedAssignmentRoleCodes.includes(roleCode);
	}

	window.LMSLookups = {
		fetchUserRoles,
		fetchProgramTypes,
		fetchSetaBodies,
		fetchUsers,
		fetchUserById,
		resolveRoleNameById,
		resolveRoleCodeById,
		isAllowedAssigneeRoleCode
	};
})(window);
