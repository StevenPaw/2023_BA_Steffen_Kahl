<?php

namespace App\CharacterDatabase;

use SilverStripe\Assets\Image;
use SilverStripe\ORM\DataObject;
use App\Users\UserData;
use SilverStripe\Security\Permission;

/**
 * Class \App\Database\Experience
 *
 * @property string $Title
 * @property int $ImageID
 * @method \SilverStripe\Assets\Image Image()
 */
class CharacterMouth extends DataObject
{
    private static $db = [
        "Title" => "Varchar(255)",
    ];

    private static $has_one = [
        "Image" => Image::class,
    ];

    private static $belongs_many = [
        "UserData" => UserData::class,
    ];

    private static $summary_fields = [
        "Title" => "Title",
    ];

    private static $field_labels = [
    ];

    private static $default_sort = "ID ASC";

    private static $table_name = "CharacterMouth";

    private static $singular_name = "Mouth";
    private static $plural_name = "Mouths";

    private static $url_segment = "character-mouths";

    public function getCMSFields()
    {
        $fields = parent::getCMSFields();
        return $fields;
    }

    public function canView($member = null)
    {
        return true;
    }

    public function canEdit($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canDelete($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canCreate($member = null, $context = [])
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }
}
